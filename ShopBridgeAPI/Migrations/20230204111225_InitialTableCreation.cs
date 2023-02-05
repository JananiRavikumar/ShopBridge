using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopBridgeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "DiscountPercentage", "Name" },
                values: new object[,]
                {
                    { 501, null, "Books" },
                    { 502, null, "Furniture" },
                    { 503, null, "Home Appliances" },
                    { 504, null, "Mobile Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 501, "A Brief History of Human Kind", "Sapiens - By Yuval Noah Harari", 460.0 },
                    { 2, 501, "An Easy and Proven way to create good habits", "Atomic Habits - By James Clear", 350.0 },
                    { 3, 501, "Timeless lessons on wealth, greed and Happiness", "The Psychology of Money - By Morgan Housel", 420.0 },
                    { 4, 501, "The two ways we make decisions", "Thinking fast and slow - By Daniel Kahneman", 460.0 },
                    { 5, 502, "Decorative set of shelves", "Wooden Wall Shelf", 2000.0 },
                    { 6, 502, "Perfect TV Unit for your home", "Wooden TV Unit", 18000.0 },
                    { 7, 502, "Seating Capacity - 8", "Homeify Sofa set", 40000.0 },
                    { 8, 502, "Wooden Shoe rack with multiple racks", "Shoe rack", 3500.0 },
                    { 9, 503, "Best company for your home maker", "XYZ Wet Grinder", 6000.0 },
                    { 10, 503, "Environment friendly Double door refrigerator", "Godrej Refrigerator", 18000.0 },
                    { 11, 504, "Type-C 18W charger", "Samsung Mobile charger", 1349.0 },
                    { 12, 504, "Best in class music", "JBL Wireless HeadPhones", 1499.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
