using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Printers_PrinterId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "PrinterId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Printers_PrinterId",
                table: "Requests",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Printers_PrinterId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "PrinterId",
                table: "Requests",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Printers_PrinterId",
                table: "Requests",
                column: "PrinterId",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
