using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class addbatchid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Coupons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Coupons");
        }
    }
}
