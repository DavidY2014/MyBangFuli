using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class modifyproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BatchId",
                table: "ProductInformations",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BatchId",
                table: "ProductInformations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
