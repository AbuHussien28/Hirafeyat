using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hirafeyat.Migrations
{
    /// <inheritdoc />
    public partial class add_brand_name_attribute_in_ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "brand_name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brand_name",
                table: "AspNetUsers");
        }
    }
}
