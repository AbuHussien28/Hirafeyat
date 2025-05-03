using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hirafeyat.Migrations
{
    /// <inheritdoc />
    public partial class QuentityAdded : Migration
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

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountCreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quentity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AccountCreatedDate",
                table: "AspNetUsers");
        }
    }
}
