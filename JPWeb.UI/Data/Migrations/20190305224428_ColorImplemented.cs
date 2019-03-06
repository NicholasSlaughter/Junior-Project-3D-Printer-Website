using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class ColorImplemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Statuses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Requests",
                newName: "DateRequested");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMade",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Requests",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "PersonalUse",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Printers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Printers",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DateMade",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PersonalUse",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Statuses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "DateRequested",
                table: "Requests",
                newName: "DateTime");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Printers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Printers",
                nullable: true);
        }
    }
}
