using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gutenburg_Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRequests_Users_UserId",
                table: "MeetingRequests");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MeetingRequests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MeetingRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MeetingRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRequests_Users_UserId",
                table: "MeetingRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingRequests_Users_UserId",
                table: "MeetingRequests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "MeetingRequests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MeetingRequests");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MeetingRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingRequests_Users_UserId",
                table: "MeetingRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
