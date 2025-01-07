using Ez.UrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ez.UrlShortener.Application.EntityTypeConfigurations
{
    public class ShortenedUrlEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ShortenedUrl>
    {
        public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
        {
            builder.HasKey(c => c.ShortCode);
            builder.Property(c => c.ShortCode).HasMaxLength(8);
            builder.HasIndex(c=>c.ShortCode).IsUnique();
            builder.Property(c => c.OriginalUrl).HasMaxLength(2048);
            builder.Property(c => c.CreatedAtUtc).HasDefaultValueSql("getutcdate()");
        }
    }
}
