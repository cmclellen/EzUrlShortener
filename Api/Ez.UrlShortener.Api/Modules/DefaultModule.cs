using Carter;
using Ez.UrlShortener.Application.Commands;
using Ez.UrlShortener.Application.Queries;
using Ez.UrlShortener.Domain.Entities;
using MediatR;
using System.Net;

namespace Ez.UrlShortener.Api.Modules
{
    public class DefaultModule : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("shorten", ShortenUrl)
                .MapToApiVersion(1)
                .WithName("ShortenUrl")
                .WithDescription("Shortens a URL that has been provided as input")
                .WithSummary("Shortens a URL that has been provided as input")
                .Produces((int)HttpStatusCode.OK, typeof(string));

            app.MapGet("{shortCode}", GetOriginalUrl)
                .MapToApiVersion(1)
                .WithName("GetOriginalUrlForShortCode")
                .WithDescription("Get the original URL for the short code")
                .WithSummary("Get the original URL for the short code")
                .Produces(((int)HttpStatusCode.Redirect), typeof(string));

            app.MapGet("urls", GetAllUrls)
                .MapToApiVersion(1)
                .WithName("GetAllUrls")
                .WithDescription("Gets all URL's")
                .WithSummary("Gets all URL's")
                .Produces(((int)HttpStatusCode.OK), typeof(string));
        }

        //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/include-metadata?view=aspnetcore-9.0&tabs=minimal-apis

        private async Task<string> ShortenUrl(string url, ISender sender)
        {
            return await sender.Send(new ShortenUrlCommand(url));
        }

        private async Task<IResult> GetOriginalUrl(string shortCode, ISender sender)
        {
            ShortenedUrl? shortenedUrl = await sender.Send(new GetOriginalUrlQuery(shortCode));
            if(shortenedUrl is null)
            {
                return Results.NotFound();
            }
            return Results.Redirect(shortenedUrl.OriginalUrl);
        }

        private async Task<string[]> GetAllUrls(ISender sender)
        {
            var shortenedUrls= await sender.Send(new GetAllUrlsQuery());
            return shortenedUrls.Select(item=>item.OriginalUrl).ToArray();
        }
    }
}
