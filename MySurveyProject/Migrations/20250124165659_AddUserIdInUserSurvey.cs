using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySurveyProject.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInUserSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersSurveys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersSurveys_UserId",
                table: "UsersSurveys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSurveys_Users_UserId",
                table: "UsersSurveys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersSurveys_Users_UserId",
                table: "UsersSurveys");

            migrationBuilder.DropIndex(
                name: "IX_UsersSurveys_UserId",
                table: "UsersSurveys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersSurveys");
        }
    }
}
