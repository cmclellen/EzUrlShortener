using Ez.UrlShortener.Domain.Exceptions;
using Ez.UrlShortener.Domain.Repositories;
using MediatR;
using System.Diagnostics.Metrics;

namespace Ez.UrlShortener.Application.Commands
{
    public record DeleteShortenedUrlCommand(string shortCode) : IRequest;

    public class DeleteShortenedUrlCommandHandler(
        IShortenedUrlRepository shortenedUrlRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteShortenedUrlCommand>
    {
        public const string METER_NAME = "UrlShortener.Api";
        private static readonly Meter Meter = new(METER_NAME);
        private static readonly Counter<int> DeletionsCounter = Meter.CreateCounter<int>("url_shortener.deletions", "Number of shortened URLs deleted");

        public async Task Handle(DeleteShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var shortenedUrl = await shortenedUrlRepository.GetByShortCodeAsync(request.shortCode);
            if (shortenedUrl == null) throw new ShortenedUrlNotFoundException(request.shortCode);
            await shortenedUrlRepository.DeleteAsync(shortenedUrl);
            await unitOfWork.SaveChangesAsync();
            DeletionsCounter.Add(1, new KeyValuePair<string, object?>("short_code", request.shortCode));
        }
    }
}
