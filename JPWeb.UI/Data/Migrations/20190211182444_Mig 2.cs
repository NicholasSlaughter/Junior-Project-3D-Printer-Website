using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "First_Name",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_Name",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicationUserId",
                table: "Requests",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ApplicationUserId",
                table: "Requests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ApplicationUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApplicationUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "First_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Last_Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
