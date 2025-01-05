using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySurveyProject.Migrations
{
    /// <inheritdoc />
    public partial class fixRelation0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "QuestionAnswers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "QuestionAnswers");
        }
    }
}
