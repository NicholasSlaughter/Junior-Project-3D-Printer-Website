using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LatestMsg",
                table: "Messages",
                newName: "latestMsg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "latestMsg",
                table: "Messages",
                newName: "LatestMsg");
        }
    }
}
