using MediatR;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetOriginalUrlQuery:IRequest<string>;

    public class GetOriginalUrlQueryHandler : IRequestHandler<GetOriginalUrlQuery, string>
    {
        public Task<string> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
