using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalR.Migrations
{
    public partial class x2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_userId",
                table: "messages",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_userId",
                table: "messages",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_userId",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_userId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "messages");
        }
    }
}
