using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Migrations
{
    public partial class MessageIDChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Message",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Message",
                newName: "MessageId");
        }
    }
}
