using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erox.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deleteColorFromCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "CardItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "CardItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
