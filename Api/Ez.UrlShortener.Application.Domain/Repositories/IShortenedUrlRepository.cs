using Ez.UrlShortener.Domain.Entities;

namespace Ez.UrlShortener.Domain.Repositories
{
    public interface IShortenedUrlRepository
    {
        Task AddAsync(ShortenedUrl shortenedUrl);
        Task DeleteAsync(ShortenedUrl shortenedUrl);
        Task<IList<ShortenedUrl>> GetAllAsync();
        Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode);
    }
}
