namespace Ez.UrlShortener.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
