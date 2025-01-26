using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySurveyProject.Migrations
{
    /// <inheritdoc />
    public partial class UserSurveyTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSurveysQuestions",
                columns: table => new
                {
                    UserSurveyQuestionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Activity = table.Column<bool>(type: "bit", nullable: false),
                    UserSurveysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveysQuestions", x => x.UserSurveyQuestionsId);
                    table.ForeignKey(
                        name: "FK_UserSurveysQuestions_UsersSurveys_UserSurveysId",
                        column: x => x.UserSurveysId,
                        principalTable: "UsersSurveys",
                        principalColumn: "UserSurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSurveyOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSurveyQuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveyOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_UserSurveyOptions_UserSurveysQuestions_UserSurveyQuestionsId",
                        column: x => x.UserSurveyQuestionsId,
                        principalTable: "UserSurveysQuestions",
                        principalColumn: "UserSurveyQuestionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveyOptions_UserSurveyQuestionsId",
                table: "UserSurveyOptions",
                column: "UserSurveyQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveysQuestions_UserSurveysId",
                table: "UserSurveysQuestions",
                column: "UserSurveysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSurveyOptions");

            migrationBuilder.DropTable(
                name: "UserSurveysQuestions");
        }
    }
}
