using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class fileExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LatestMsg",
                table: "Messages",
                newName: "latestMsg");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectFilePath",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "latestMsg",
                table: "Messages",
                newName: "LatestMsg");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ProjectFilePath",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
