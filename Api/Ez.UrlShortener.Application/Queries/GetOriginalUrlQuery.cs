using Ez.UrlShortener.Application.Diagnostics;
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
        private static readonly Counter<int> CacheHitCounter = AppMeters.ApiMeter.CreateCounter<int>("url_shortener.cache_hit", "Number of cache hits");
        private static readonly Counter<int> RedirectsCounter = AppMeters.ApiMeter.CreateCounter<int>("url_shortener.redirects", "Number of redirects");
        private static readonly Counter<int> FailedRedirectsCounter = AppMeters.ApiMeter.CreateCounter<int>("url_shortener.failed_redirects", "Number of failed redirects");

        public async Task<string?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
        {
            bool cacheHit = true;
            var originalUrl = await hybridCache.GetOrCreateAsync(request.shortCode, async token =>
            {
                var originalUrl = await shortenedUrlRepository.GetByShortCodeAsync(request.shortCode);
                cacheHit = false;
                return originalUrl?.OriginalUrl;
            });

            if(cacheHit)
            {
                CacheHitCounter.Add(1, new KeyValuePair<string, object?>("short_code", request.shortCode));
            }

            if (originalUrl is null)
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
