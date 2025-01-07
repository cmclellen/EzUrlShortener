using MediatR;

namespace Ez.UrlShortener.Application.Commands
{
    public record ShortenUrlCommand(string urlToShorten) : IRequest<string>;

    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, string>
    {
        public Task<string> Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
        {
            if(!Uri.TryCreate(request.urlToShorten, UriKind.Absolute, out _))
            {
                throw new Exception("Invalid ULR format");
            }
            
            return Task.FromResult("abc");
        }
    }
}
