using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sqlserver-password", secret: true);
var sqlServer = builder.AddSqlServer("sqlserver", password: sqlPassword, port: 63814)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithContainerName("url-shortener-sqlserver");

var db = sqlServer.AddDatabase("url-shortener-db", "UrlShortener");

var redis = builder.AddRedis("redis");

var urlShortnerUrl = builder.AddProject<Projects.Ez_UrlShortener_Api>("ez-urlshortener-api")
    .WithReference(db)
    .WaitFor(db)
    .WithReference(redis)
    .WaitFor(redis);

builder.AddNpmApp("react", "../../UI", "dev")
    .WithReference(urlShortnerUrl)
    .WaitFor(urlShortnerUrl)    
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
