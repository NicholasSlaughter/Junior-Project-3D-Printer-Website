using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class MessageHub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "msg");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Messages",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "MessageTitle",
                table: "Messages",
                newName: "hubTitle");

            migrationBuilder.RenameColumn(
                name: "messageId",
                table: "Messages",
                newName: "messageHubId");

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageHubId = table.Column<int>(nullable: false),
                    messageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sender = table.Column<string>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    timeSent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.messageId);
                    table.ForeignKey(
                        name: "FK_Message_Messages_messageHubId",
                        column: x => x.messageHubId,
                        principalTable: "Messages",
                        principalColumn: "messageHubId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_messageHubId",
                table: "Message",
                column: "messageHubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.RenameColumn(
                name: "hubTitle",
                table: "Messages",
                newName: "MessageTitle");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Messages",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "messageHubId",
                table: "Messages",
                newName: "messageId");

            migrationBuilder.CreateTable(
                name: "msg",
                columns: table => new
                {
                    msgId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    _msg = table.Column<string>(nullable: true),
                    messageId = table.Column<int>(nullable: false),
                    timeSent = table.Column<DateTime>(nullable: false),
                    user = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msg", x => x.msgId);
                    table.ForeignKey(
                        name: "FK_msg_Messages_messageId",
                        column: x => x.messageId,
                        principalTable: "Messages",
                        principalColumn: "messageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_msg_messageId",
                table: "msg",
                column: "messageId");
        }
    }
}
