using MediatR;

namespace Ez.UrlShortener.Application.Queries
{
    public record GetAllUrlsQuery : IRequest<string[]>;

    public class GetAllUrlsQueryHandler : IRequestHandler<GetAllUrlsQuery, string[]>
    {
        public Task<string[]> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new [] { "ff", "dd" });
        }
    }
}
