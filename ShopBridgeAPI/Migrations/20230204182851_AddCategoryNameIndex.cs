﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopBridgeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_Name",
                table: "Category");
        }
    }
}
