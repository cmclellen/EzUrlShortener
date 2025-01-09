using Ardalis.GuardClauses;

namespace Ez.UrlShortener.Domain.Exceptions
{
    public class ShortenedUrlNotFoundException : Exception
    {
        public ShortenedUrlNotFoundException(string shortCode) : base($"Shortened URL not found for short code {shortCode}.")
        {
            Guard.Against.NullOrEmpty(shortCode, nameof(shortCode));
            ShortCode = shortCode;
        }
        public string ShortCode { get; }
    }
}
