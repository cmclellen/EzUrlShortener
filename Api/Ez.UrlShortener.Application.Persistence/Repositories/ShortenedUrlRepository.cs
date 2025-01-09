using Ez.UrlShortener.Application.Persistence;
using Ez.UrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ez.UrlShortener.Persistence.Repositories
{
    public class ShortenedUrlRepository(UrlShortenerDbContext dbContext) : Domain.Repositories.IShortenedUrlRepository
    {
        public async Task AddAsync(ShortenedUrl shortenedUrl)
        {
            await dbContext.ShortenedUrls.AddAsync(shortenedUrl);
        }

        public Task DeleteAsync(ShortenedUrl shortenedUrl)
        {
            dbContext.Remove(shortenedUrl);
            return Task.CompletedTask;
        }

        public async Task<IList<ShortenedUrl>> GetAllAsync()
        {
            return await dbContext.ShortenedUrls.ToListAsync();
        }

        public async Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode)
        {
            return await dbContext.ShortenedUrls.FirstOrDefaultAsync(item => item.ShortCode == shortCode);
        }
    }
}
