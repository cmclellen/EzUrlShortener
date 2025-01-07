using Ez.UrlShortener.Domain.Entities;
using Ez.UrlShortener.Domain.Repositories;
using MediatR;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetOriginalUrlQuery(string shortCode):IRequest<ShortenedUrl?>;

    public class GetOriginalUrlQueryHandler(IShortenedUrlRepository shortenedUrlRepository) : IRequestHandler<GetOriginalUrlQuery, ShortenedUrl?>
    {
        public async Task<ShortenedUrl?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
        {
            return await shortenedUrlRepository.GetByShortCodeAsync(request.shortCode);
        }
    }
}
