using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class updatebatchid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchCode",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Banners");

            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Banners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BannerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BannerId = table.Column<int>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerDetails_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannerDetails_BannerId",
                table: "BannerDetails",
                column: "BannerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerDetails");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Banners");

            migrationBuilder.AddColumn<string>(
                name: "BatchCode",
                table: "Banners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Banners",
                nullable: true);
        }
    }
}
