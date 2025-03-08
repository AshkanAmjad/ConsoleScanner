using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScannerAPIProject.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuPages",
                columns: table => new
                {
                    MenuPageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPages", x => x.MenuPageId);
                });

            migrationBuilder.CreateTable(
                name: "MenuPageApis",
                columns: table => new
                {
                    MemuPageApiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuPageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPageApis", x => x.MemuPageApiId);
                    table.ForeignKey(
                        name: "FK_MenuPageApis_MenuPages_MenuPageId",
                        column: x => x.MenuPageId,
                        principalTable: "MenuPages",
                        principalColumn: "MenuPageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuPageApis_MenuPageId",
                table: "MenuPageApis",
                column: "MenuPageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPageApis");

            migrationBuilder.DropTable(
                name: "MenuPages");
        }
    }
}
