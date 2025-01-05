using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySurveyProject.Migrations
{
    /// <inheritdoc />
    public partial class SurveyAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    SurveyAnsweId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.SurveyAnsweId);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyId",
                table: "SurveyAnswers",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyAnswers");
        }
    }
}
