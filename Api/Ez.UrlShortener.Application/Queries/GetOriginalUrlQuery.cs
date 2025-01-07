using MediatR;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetOriginalUrlQuery(string shortCode):IRequest<string>;

    public class GetOriginalUrlQueryHandler : IRequestHandler<GetOriginalUrlQuery, string>
    {
        public Task<string> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("long url");
        }
    }
}
