using Ez.UrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ez.UrlShortener.Application.Persistence
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
        {

        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>().HasData(
                    new ShortenedUrl
                    {
                        ShortCode = "abc123",
                        OriginalUrl = "https://www.google.com",
                        CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 0, DateTimeKind.Utc)
                    },
                    new ShortenedUrl
                    {
                        ShortCode = "abc124",
                        OriginalUrl = "https://www.news24.com",
                        CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 1, DateTimeKind.Utc)
                    },
                    new ShortenedUrl
                    {
                        ShortCode = "abc125",
                        OriginalUrl = "https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding",
                        CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 2, DateTimeKind.Utc)
                    },
                    new ShortenedUrl
                    {
                        ShortCode = "abc126",
                        OriginalUrl = "https://en.wikipedia.org/wiki/42_(number)",
                        CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 3, DateTimeKind.Utc)
                    });
        }
    }
}
