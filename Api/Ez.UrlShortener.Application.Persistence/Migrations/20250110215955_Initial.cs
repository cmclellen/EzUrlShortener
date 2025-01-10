using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ez.UrlShortener.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortenedUrls",
                columns: table => new
                {
                    ShortCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    OriginalUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenedUrls", x => x.ShortCode);
                });

            migrationBuilder.InsertData(
                table: "ShortenedUrls",
                columns: new[] { "ShortCode", "CreatedAtUtc", "OriginalUrl" },
                values: new object[,]
                {
                    { "abc123", new DateTime(2025, 1, 1, 15, 0, 0, 0, DateTimeKind.Utc), "https://www.google.com" },
                    { "abc124", new DateTime(2025, 1, 1, 15, 0, 1, 0, DateTimeKind.Utc), "https://www.news24.com" },
                    { "abc125", new DateTime(2025, 1, 1, 15, 0, 2, 0, DateTimeKind.Utc), "https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding" },
                    { "abc126", new DateTime(2025, 1, 1, 15, 0, 3, 0, DateTimeKind.Utc), "https://en.wikipedia.org/wiki/42_(number)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortenedUrls_ShortCode",
                table: "ShortenedUrls",
                column: "ShortCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortenedUrls");
        }
    }
}
