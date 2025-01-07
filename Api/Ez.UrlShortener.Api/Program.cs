using Ez.UrlShortener.Api.Extensions;
using Ez.UrlShortener.Application.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Ez.UrlShortener.Application.AssemblyReference.Assembly));

builder.AddServiceDefaults();

builder.AddRedisDistributedCache("redis");
builder.AddSqlServerDbContext<UrlShortenerDbContext>(connectionName: "url-shortener-db");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .ConfigureJsonOptionsEx()
    .AddApiVersioningEx()
    .AddGlobalExceptionHandler();

builder.Services.Scan(scan => scan
    .FromAssemblies(Ez.UrlShortener.Domain.AssemblyReference.Assembly, Ez.UrlShortener.Persistence.AssemblyReference.Assembly)
    .AddClasses(false)
    .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithTransientLifetime());

#pragma warning disable EXTEXP0018 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
builder.Services.AddHybridCache();
#pragma warning restore EXTEXP0018 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

var app = builder.Build();

app.MapDefaultEndpoints()
    .UseApiVersioningEx()
    .UseGlobalExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
        options.RoutePrefix = string.Empty;        
    });
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UrlShortenerDbContext>();
    var db = dbContext.Database;
    db.EnsureDeleted();
    db.Migrate();
}

app.Run();