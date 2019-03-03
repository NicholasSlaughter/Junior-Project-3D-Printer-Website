using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class msg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageBody",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "msg",
                columns: table => new
                {
                    msgId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    _msg = table.Column<string>(nullable: true),
                    messageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msg", x => x.msgId);
                    table.ForeignKey(
                        name: "FK_msg_Messages_messageId",
                        column: x => x.messageId,
                        principalTable: "Messages",
                        principalColumn: "messageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_msg_messageId",
                table: "msg",
                column: "messageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "msg");

            migrationBuilder.AddColumn<string>(
                name: "MessageBody",
                table: "Messages",
                nullable: true);
        }
    }
}
