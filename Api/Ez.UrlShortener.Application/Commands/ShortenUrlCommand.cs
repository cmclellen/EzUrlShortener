using Ez.UrlShortener.Domain.Entities;
using Ez.UrlShortener.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace Ez.UrlShortener.Application.Commands
{
    public record ShortenUrlCommand(string urlToShorten) : IRequest<string>;

    public class ShortenUrlCommandHandler(
        ILogger<ShortenUrlCommandHandler> logger, 
        //HybridCache hybridCache,
        IShortenedUrlRepository shortenedUrlRepository, 
        IUnitOfWork unitOfWork) : IRequestHandler<ShortenUrlCommand, string>
    {
        private const int MaxRetries = 3;
                
        private string GenerateShortCode()
        {
            const int length = 7;
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<string> Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
        {
            for(var attempt = 0; attempt < MaxRetries; attempt++) { 
                try
                {
                    if (!Uri.TryCreate(request.urlToShorten, UriKind.Absolute, out _))
                    {
                        throw new Exception("Invalid ULR format");
                    }

                    var shortCode = GenerateShortCode();
                    await shortenedUrlRepository.AddAsync(new ShortenedUrl
                    {
                        ShortCode = shortCode,
                        OriginalUrl = request.urlToShorten
                    });
                    await unitOfWork.SaveChangesAsync();

                    //await hybridCache.SetAsync(shortCode, request.urlToShorten);

                    return shortCode;
                }
                catch(DbException ex ) when (ex.ErrorCode == 2627)
                {
                    if(attempt == MaxRetries)
                    {
                        logger.LogError(ex, "Failed generating unique short code after {MaxRetries} attempts.", MaxRetries);
                        throw new InvalidOperationException("Failed generating unique short code.", ex);
                    }
                    logger.LogWarning("Short code exists. Retrying... (attempt {Attempt} of {MaxRetries})", attempt, MaxRetries);
                }                
            }
            throw new InvalidOperationException("Failed generating unique short code.");
        }
    }
}
