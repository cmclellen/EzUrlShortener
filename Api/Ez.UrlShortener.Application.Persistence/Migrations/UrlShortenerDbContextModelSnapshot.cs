﻿// <auto-generated />
using System;
using Ez.UrlShortener.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ez.UrlShortener.Persistence.Migrations
{
    [DbContext(typeof(UrlShortenerDbContext))]
    partial class UrlShortenerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ez.UrlShortener.Domain.Entities.ShortenedUrl", b =>
                {
                    b.Property<string>("ShortCode")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("ShortCode");

                    b.HasIndex("ShortCode")
                        .IsUnique();

                    b.ToTable("ShortenedUrls");

                    b.HasData(
                        new
                        {
                            ShortCode = "abc123",
                            CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                            OriginalUrl = "https://www.google.com"
                        },
                        new
                        {
                            ShortCode = "abc124",
                            CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 1, 0, DateTimeKind.Utc),
                            OriginalUrl = "https://www.news24.com"
                        },
                        new
                        {
                            ShortCode = "abc125",
                            CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 2, 0, DateTimeKind.Utc),
                            OriginalUrl = "https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding"
                        },
                        new
                        {
                            ShortCode = "abc126",
                            CreatedAtUtc = new DateTime(2025, 1, 1, 15, 0, 3, 0, DateTimeKind.Utc),
                            OriginalUrl = "https://en.wikipedia.org/wiki/42_(number)"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
