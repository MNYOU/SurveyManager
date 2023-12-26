using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConf5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_Questions_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_Questions_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_Questions_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_Questions_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
