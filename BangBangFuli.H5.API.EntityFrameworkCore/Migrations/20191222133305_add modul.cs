using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BangBangFuli.H5.API.EntityFrameworkCore.Migrations
{
    public partial class addmodul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModuleInfos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleJurisdictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserRoleID = table.Column<int>(nullable: false),
                    ModuleID = table.Column<int>(nullable: false),
                    IsQuery = table.Column<bool>(nullable: false),
                    IsAdd = table.Column<bool>(nullable: false),
                    IsEdit = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsAssignment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleJurisdictions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleInfos");

            migrationBuilder.DropTable(
                name: "UserRoleJurisdictions");
        }
    }
}
