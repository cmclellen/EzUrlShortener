using Ez.UrlShortener.Domain.Entities;
using Ez.UrlShortener.Domain.Repositories;
using MediatR;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetAllUrlsQuery : IRequest<IList<ShortenedUrl>>;

    public class GetAllUrlsQueryHandler(IShortenedUrlRepository shortenedUrlRepository) : IRequestHandler<GetAllUrlsQuery, IList<ShortenedUrl>>
    {
        public async Task<IList<ShortenedUrl>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
        {
            return await shortenedUrlRepository.GetAllAsync();
        }
    }
}
