using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vitrina.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Role = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Audio", new DateTime(2025, 1, 5, 9, 0, 0, 0, DateTimeKind.Utc), "Premium over-ear headphones with active noise cancellation and all-day battery life.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=1200&q=80", true, "Wireless Noise-Canceling Headphones", 249.99m, 35, null },
                    { 2, "Accessories", new DateTime(2025, 1, 12, 10, 30, 0, 0, DateTimeKind.Utc), "Slim 15.6-inch USB-C portable monitor for working on the go.", "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?auto=format&fit=crop&w=1200&q=80", true, "4K Portable Monitor", 329.00m, 18, null },
                    { 3, "Computing", new DateTime(2025, 1, 20, 14, 0, 0, 0, DateTimeKind.Utc), "Compact mechanical keyboard with hot-swappable switches and RGB backlight.", "https://images.unsplash.com/photo-1511467687858-23d96c32e4ae?auto=format&fit=crop&w=1200&q=80", true, "Mechanical Keyboard", 129.50m, 42, null },
                    { 4, "Wearables", new DateTime(2025, 2, 1, 8, 15, 0, 0, DateTimeKind.Utc), "Health-focused smartwatch with heart rate tracking, GPS, and sleep analytics.", "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=1200&q=80", true, "Smart Fitness Watch", 199.99m, 27, null },
                    { 5, "Office", new DateTime(2025, 2, 10, 11, 45, 0, 0, DateTimeKind.Utc), "Adjustable office chair with lumbar support and breathable mesh back.", "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?auto=format&fit=crop&w=1200&q=80", true, "Ergonomic Office Chair", 289.00m, 12, null },
                    { 6, "Accessories", new DateTime(2025, 2, 18, 16, 20, 0, 0, DateTimeKind.Utc), "Fast charging stand compatible with modern phones and earbuds cases.", "https://m.media-amazon.com/images/I/61eWPXeHV-L._AC_SL1500_.jpg", true, "Wireless Charging Stand", 39.90m, 65, null },
                    { 7, "Computing", new DateTime(2025, 3, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Multi-port docking station with HDMI, Ethernet, and card reader support.", "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?auto=format&fit=crop&w=1200&q=80", true, "USB-C Docking Station", 149.95m, 21, null },
                    { 8, "Audio", new DateTime(2025, 3, 14, 13, 30, 0, 0, DateTimeKind.Utc), "Portable speaker with balanced sound, water resistance, and 20-hour battery.", "https://images.unsplash.com/photo-1518441902117-f0a6a3f0d5b2?auto=format&fit=crop&w=1200&q=80", true, "Bluetooth Speaker", 89.99m, 54, null },
                    { 9, "Office", new DateTime(2025, 4, 1, 9, 40, 0, 0, DateTimeKind.Utc), "LED desk lamp with adjustable brightness and built-in USB charging port.", "https://m.media-amazon.com/images/I/81qhD0HhUHL._AC_SL1500_.jpg", true, "Desk Lamp with USB Port", 54.75m, 40, null },
                    { 10, "Computing", new DateTime(2025, 4, 12, 15, 0, 0, 0, DateTimeKind.Utc), "High precision gaming mouse with programmable buttons and low-latency sensor.", "https://images.unsplash.com/photo-1527814050087-3793815479db?auto=format&fit=crop&w=1200&q=80", true, "Gaming Mouse", 74.25m, 48, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 8, 0, 0, 0, DateTimeKind.Utc), "admin@vitrina.dev", "Admin User", "PBKDF2$100000$M2sLSVWCKjmNAWrhiLff2Q==$c3UWlPye+02m+RWkGYCP9nqGU1Cmb5MpNm1D23KpqK8=", (byte)1, "admin" },
                    { 2, new DateTime(2025, 1, 1, 8, 5, 0, 0, DateTimeKind.Utc), "user@vitrina.dev", "Regular User", "PBKDF2$100000$yy8A999moS2/LzRxBqAfZQ==$uOMR3XbgOSu80whbJREikbZrV+3g6HfN1OsY0p1RsLo=", (byte)2, "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "dbo",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "dbo",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
