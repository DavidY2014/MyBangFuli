using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class1",
                table: "ProductInformations");

            migrationBuilder.DropColumn(
                name: "Class2",
                table: "ProductInformations");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProductInformations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProductInformations");

            migrationBuilder.AddColumn<int>(
                name: "Class1",
                table: "ProductInformations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Class2",
                table: "ProductInformations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
