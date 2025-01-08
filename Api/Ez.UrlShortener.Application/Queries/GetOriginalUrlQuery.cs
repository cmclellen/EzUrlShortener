using Ez.UrlShortener.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetOriginalUrlQuery(string shortCode):IRequest<string?>;

    public class GetOriginalUrlQueryHandler(
        HybridCache hybridCache,
        IShortenedUrlRepository shortenedUrlRepository) : IRequestHandler<GetOriginalUrlQuery, string?>
    {
        public async Task<string?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
        {
            var originalUrl = await hybridCache.GetOrCreateAsync(request.shortCode, async token =>
            {
                var originalUrl =  await shortenedUrlRepository.GetByShortCodeAsync(request.shortCode);
                return originalUrl?.OriginalUrl;
            });
            return originalUrl;
        }
    }
}
