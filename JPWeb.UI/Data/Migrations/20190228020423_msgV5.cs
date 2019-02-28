using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class msgV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Messages",
                newName: "LatestMsg");

            migrationBuilder.AddColumn<DateTime>(
                name: "timeSent",
                table: "msg",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "timeSent",
                table: "msg");

            migrationBuilder.RenameColumn(
                name: "LatestMsg",
                table: "Messages",
                newName: "CreationDate");
        }
    }
}
