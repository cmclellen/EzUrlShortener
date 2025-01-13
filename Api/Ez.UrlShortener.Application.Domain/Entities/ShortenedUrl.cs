namespace Ez.UrlShortener.Domain.Entities
{
    public class ShortenedUrl
    {
        public required string ShortCode { get; set; }
        public required string OriginalUrl { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
