using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sqlserver-password", secret: true);
var sqlServer = builder.AddSqlServer("sqlserver", password: sqlPassword, port: 63814)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithContainerName("url-shortener-sqlserver");

var db = sqlServer.AddDatabase("url-shortener-db", "UrlShortener");

var redis = builder.AddRedis("redis");

builder.AddProject<Projects.Ez_UrlShortener_Api>("ez-urlshortener-api")
    .WithReference(db)
    .WaitFor(db)
    .WithReference(redis)
    .WaitFor(redis);

builder.Build().Run();
