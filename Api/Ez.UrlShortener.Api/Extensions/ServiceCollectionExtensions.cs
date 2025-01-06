using Asp.Versioning;
using Carter;
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
    }
}
