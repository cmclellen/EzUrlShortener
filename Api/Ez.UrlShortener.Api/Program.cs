using Ez.UrlShortener.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Ez.UrlShortener.Application.AssemblyReference.Assembly));

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .ConfigureJsonOptionsEx()
    .AddApiVersioningEx()
    .AddGlobalExceptionHandler();

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

app.Run();