using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInStock",
                table: "ProductInformations");

            migrationBuilder.AddColumn<int>(
                name: "StockType",
                table: "ProductInformations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockType",
                table: "ProductInformations");

            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                table: "ProductInformations",
                nullable: false,
                defaultValue: false);
        }
    }
}
