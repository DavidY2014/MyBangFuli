using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "BatchInformations");

            migrationBuilder.AlterColumn<int>(
                name: "BatchId",
                table: "ProductInformations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BatchId",
                table: "Banners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BatchId",
                table: "ProductInformations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "BatchId",
                table: "BatchInformations",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BatchId",
                table: "Banners",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
