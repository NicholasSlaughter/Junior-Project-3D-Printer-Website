using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class derp4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Color_ColorId",
                table: "Printers");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Statuses_StatusId",
                table: "Printers");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Printers_ColorId",
                table: "Printers");

            migrationBuilder.DropIndex(
                name: "IX_Printers_StatusId",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "LatestMsg",
                table: "Messages",
                newName: "latestMsg");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Printers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Printers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "latestMsg",
                table: "Messages",
                newName: "LatestMsg");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Printers_ColorId",
                table: "Printers",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_StatusId",
                table: "Printers",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Color_ColorId",
                table: "Printers",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_Statuses_StatusId",
                table: "Printers",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
