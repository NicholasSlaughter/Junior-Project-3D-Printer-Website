using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class ERDv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "latestMsg",
                table: "Messages",
                newName: "TimeSent");

            migrationBuilder.RenameColumn(
                name: "hubTitle",
                table: "Messages",
                newName: "SenderId1");

            migrationBuilder.RenameColumn(
                name: "messageHubId",
                table: "Messages",
                newName: "MessageId");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId1",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requestId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId1",
                table: "Messages",
                column: "SenderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_requestId",
                table: "Messages",
                column: "requestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId1",
                table: "Messages",
                column: "SenderId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Requests_requestId",
                table: "Messages",
                column: "requestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Requests_requestId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_requestId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "requestId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "TimeSent",
                table: "Messages",
                newName: "latestMsg");

            migrationBuilder.RenameColumn(
                name: "SenderId1",
                table: "Messages",
                newName: "hubTitle");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Messages",
                newName: "messageHubId");

            migrationBuilder.AlterColumn<string>(
                name: "hubTitle",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Messages",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    body = table.Column<string>(nullable: true),
                    messageHubId = table.Column<int>(nullable: false),
                    sender = table.Column<string>(nullable: true),
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
    }
}
