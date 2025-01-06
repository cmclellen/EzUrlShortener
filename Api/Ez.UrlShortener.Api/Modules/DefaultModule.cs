using Carter;
using Microsoft.AspNetCore.Mvc;

namespace Ez.UrlShortener.Api.Modules
{
    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }


    public class DefaultModule : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/weatherforecast", GetWeatherForecast)
                .MapToApiVersion(1)
                .WithName("GetWeatherForecast");
        }

        /// <summary>
        /// Gets weather forecast.
        /// </summary>
        /// <returns>The weather forecast</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/weatherforecast
        ///
        /// </remarks>
        /// <response code="200">Returns the weather forecast</response>
        /// <response code="500">An unexpected error has occurred</response>
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]
        [Produces("application/json")]
        private WeatherForecast[] GetWeatherForecast()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
            return forecast;
        }
    }
}
