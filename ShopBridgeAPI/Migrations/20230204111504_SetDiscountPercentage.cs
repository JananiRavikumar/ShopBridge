using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopBridgeAPI.Migrations
{
    /// <inheritdoc />
    public partial class SetDiscountPercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 501,
                column: "DiscountPercentage",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 504,
                column: "DiscountPercentage",
                value: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 501,
                column: "DiscountPercentage",
                value: null);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 504,
                column: "DiscountPercentage",
                value: null);
        }
    }
}
