using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Data.Migrations
{
    /// <inheritdoc />
    public partial class SyncSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/61eWPXeHV-L._AC_SL1500_.jpg");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/I/81qhD0HhUHL._AC_SL1500_.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1612817159949-3f7d9b07c9c6?auto=format&fit=crop&w=1200&q=80");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1518441902117-f0a6a3f0d5b2?auto=format&fit=crop&w=1200&q=80");
        }
    }
}
