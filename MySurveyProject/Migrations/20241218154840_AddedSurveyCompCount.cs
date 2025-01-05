using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySurveyProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedSurveyCompCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyCompletionCount",
                table: "Surveys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurveyCompletionCount",
                table: "Surveys");
        }
    }
}
