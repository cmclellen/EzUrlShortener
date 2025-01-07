using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ez.UrlShortener.Application.Persistence
{
    public class UrlShortenerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<UrlShortenerDbContext>
    {
        public UrlShortenerDbContext CreateDbContext(string[] args)
        {
            //var connectionString = Environment.GetEnvironmentVariable("EFCORETOOLSDB");

            var connectionString = "Server=127.0.0.1,63814;User Id=sa;Password={+C2CYhJ1D5Q(k1bNXT{aD;Database=UrlShortener;TrustServerCertificate=true";
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set " +
                "in the 'EFCORETOOLSDB' environment variable.");

            var options = new DbContextOptionsBuilder<UrlShortenerDbContext>()
               .UseSqlServer(connectionString)
               .Options;
            return new UrlShortenerDbContext(options);
        }
    }
}
