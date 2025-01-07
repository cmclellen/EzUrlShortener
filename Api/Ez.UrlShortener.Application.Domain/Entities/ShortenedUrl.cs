namespace Ez.UrlShortener.Domain.Entities
{
    public class ShortenedUrl
    {
        public string ShortCode { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
