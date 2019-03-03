using Microsoft.EntityFrameworkCore.Migrations;

namespace JPWeb.UI.Data.Migrations
{
    public partial class msgV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_msg_Messages_messageId",
                table: "msg");

            migrationBuilder.AlterColumn<int>(
                name: "messageId",
                table: "msg",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_msg_Messages_messageId",
                table: "msg",
                column: "messageId",
                principalTable: "Messages",
                principalColumn: "messageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_msg_Messages_messageId",
                table: "msg");

            migrationBuilder.AlterColumn<int>(
                name: "messageId",
                table: "msg",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_msg_Messages_messageId",
                table: "msg",
                column: "messageId",
                principalTable: "Messages",
                principalColumn: "messageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
