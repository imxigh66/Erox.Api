using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erox.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deleteColorFromOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
