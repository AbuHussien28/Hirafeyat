using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hirafeyat.Migrations
{
    /// <inheritdoc />
    public partial class addingQuentityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quentity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quentity",
                table: "Products");
        }
    }
}
