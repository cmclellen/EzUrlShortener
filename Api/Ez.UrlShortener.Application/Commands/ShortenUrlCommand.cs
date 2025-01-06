using MediatR;

namespace Ez.UrlShortener.Application.Commands
{
    public record ShortenUrlCommand : IRequest;

    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand>
    {
        public Task Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
