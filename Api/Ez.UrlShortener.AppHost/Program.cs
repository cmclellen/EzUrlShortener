var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Ez_UrlShortener_Api>("ez-urlshortener-api");

builder.Build().Run();
