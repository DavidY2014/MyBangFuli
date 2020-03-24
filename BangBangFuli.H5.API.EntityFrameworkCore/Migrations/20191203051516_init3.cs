using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductInformations");

            migrationBuilder.AddColumn<int>(
                name: "ProductStatus",
                table: "ProductInformations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductStatus",
                table: "ProductInformations");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductInformations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
