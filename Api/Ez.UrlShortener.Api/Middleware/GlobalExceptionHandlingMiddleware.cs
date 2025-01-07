using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Ez.UrlShortener.Api.Middleware
{
    public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An enexpected error occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                ProblemDetails details = new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server has occurred"
                };

                var json = JsonSerializer.Serialize(details);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
