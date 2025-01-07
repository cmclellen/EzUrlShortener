using Asp.Versioning;
using Carter;

namespace Ez.UrlShortener.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseApiVersioningEx(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .Build();
            var apiMapGroup = app.MapGroup("api/v{version:apiVersion}")
                .WithApiVersionSet(apiVersionSet);
            //apiMapGroup.RequireAuthorization();
            apiMapGroup.MapCarter();
            return app;
        }
    

        public static WebApplication UseGlobalExceptionHandler(this WebApplication app)
        {
            //app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseExceptionHandler();
            app.UseStatusCodePages();
            return app;
        }
    }
}

