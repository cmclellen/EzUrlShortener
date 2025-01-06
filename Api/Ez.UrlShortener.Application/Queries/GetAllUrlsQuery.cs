using MediatR;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetAllUrlsQuery : IRequest<IList<string>>;

    public class GetAllUrlsQueryHandler : IRequestHandler<GetAllUrlsQuery, IList<string>>
    {
        public Task<IList<string>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
