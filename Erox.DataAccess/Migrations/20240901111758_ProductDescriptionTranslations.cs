using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erox.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductDescriptionTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductDescriptionTranslations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDescriptionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDescriptionTranslations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescriptionTranslations_Language",
                table: "ProductDescriptionTranslations",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescriptionTranslations_ProductId",
                table: "ProductDescriptionTranslations",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDescriptionTranslations");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
