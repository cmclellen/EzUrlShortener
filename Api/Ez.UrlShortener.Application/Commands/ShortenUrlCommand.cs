using Ez.UrlShortener.Domain.Entities;
using Ez.UrlShortener.Domain.Repositories;
using MediatR;
using System.Data.Common;

namespace Ez.UrlShortener.Application.Commands
{
    public record ShortenUrlCommand(string urlToShorten) : IRequest<string>;

    public class ShortenUrlCommandHandler(IShortenedUrlRepository shortenedUrlRepository, IUnitOfWork unitOfWork) : IRequestHandler<ShortenUrlCommand, string>
    {
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

                return shortCode;
            }
            catch(DbException ex ) when (ex.ErrorCode == 2627)
            {
                throw new Exception("Short code already exists");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while shortening the URL");
            }
        }
    }
}
