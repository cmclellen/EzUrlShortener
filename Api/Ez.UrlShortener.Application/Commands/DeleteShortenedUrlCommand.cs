using Ez.UrlShortener.Domain.Exceptions;
using Ez.UrlShortener.Domain.Repositories;
using MediatR;

namespace Ez.UrlShortener.Application.Commands
{
    public record DeleteShortenedUrlCommand(string shortCode) : IRequest;

    public class DeleteShortenedUrlCommandHandler(IShortenedUrlRepository shortenedUrlRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteShortenedUrlCommand>
    {
        public async Task Handle(DeleteShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var shortenedUrl = await shortenedUrlRepository.GetByShortCodeAsync(request.shortCode);
            if (shortenedUrl == null) throw new ShortenedUrlNotFoundException(request.shortCode);
            await shortenedUrlRepository.DeleteAsync(shortenedUrl);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
