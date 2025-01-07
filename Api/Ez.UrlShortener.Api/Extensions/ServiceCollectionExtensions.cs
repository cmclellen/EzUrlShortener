using Asp.Versioning;
using Carter;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ez.UrlShortener.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection ConfigureJsonOptionsEx(this IServiceCollection services)
        {
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            return services;
        }

        public static IServiceCollection AddApiVersioningEx(this IServiceCollection services)
        {
            services
                .AddCarter()
                .AddApiVersioning(options => {
                    options.DefaultApiVersion = new ApiVersion(1);
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddApiExplorer(options => {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });
            return services;
        }

        public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
        {
            //services.AddTransient<GlobalExceptionHandlingMiddleware>();
            services.AddProblemDetails(o => {
                o.CustomizeProblemDetails = context =>
                {
                    var httpContext = context.HttpContext;
                    Activity? activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";
                    context.ProblemDetails.Extensions.Add("requestId", httpContext.TraceIdentifier);
                    //context.ProblemDetails.Extensions.Add("traceId", activity?.Id);
                };
            });
            services.AddExceptionHandler<Exceptions.GlobalExceptionHandler>();
            return services;
        }
    }
}
