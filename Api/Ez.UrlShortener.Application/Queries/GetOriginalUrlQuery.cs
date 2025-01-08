using Ez.UrlShortener.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using System.Diagnostics.Metrics;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetOriginalUrlQuery(string shortCode):IRequest<string?>;

    public class GetOriginalUrlQueryHandler(
        HybridCache hybridCache,
        IShortenedUrlRepository shortenedUrlRepository) : IRequestHandler<GetOriginalUrlQuery, string?>
    {
        public const string METER_NAME = "UrlShortener.Api";
        private static readonly Meter Meter = new(METER_NAME);
        private static readonly Counter<int> RedirectsCounter = Meter.CreateCounter<int>("url_shortener.redirects", "Number of redirects");
        private static readonly Counter<int> FailedRedirectsCounter = Meter.CreateCounter<int>("url_shortener.failed_redirects", "Number of failed redirects");

        public async Task<string?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
        {
            var originalUrl = await hybridCache.GetOrCreateAsync(request.shortCode, async token =>
            {
                var originalUrl =  await shortenedUrlRepository.GetByShortCodeAsync(request.shortCode);
                return originalUrl?.OriginalUrl;
            });

            if(originalUrl is null)
            {
                FailedRedirectsCounter.Add(1, new KeyValuePair<string, object?>("short_code", request.shortCode));
            }
            else
            {
                RedirectsCounter.Add(1, new KeyValuePair<string, object?>("short_code", request.shortCode));
            }

            return originalUrl;
        }
    }
}
