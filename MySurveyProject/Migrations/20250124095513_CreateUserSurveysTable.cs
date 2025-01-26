using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySurveyProject.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserSurveysTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersSurveys",
                columns: table => new
                {
                    UserSurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activity = table.Column<bool>(type: "bit", nullable: false),
                    Completable = table.Column<bool>(type: "bit", nullable: false),
                    SurveyCompletionCount = table.Column<int>(type: "int", nullable: false),
                    UniuqeLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSurveys", x => x.UserSurveyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersSurveys");
        }
    }
}
