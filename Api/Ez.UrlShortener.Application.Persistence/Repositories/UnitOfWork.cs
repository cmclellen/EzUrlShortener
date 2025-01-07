using Ez.UrlShortener.Application.Persistence;
using Ez.UrlShortener.Domain.Repositories;

namespace Ez.UrlShortener.Persistence.Repositories
{
    public class UnitOfWork(UrlShortenerDbContext dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
