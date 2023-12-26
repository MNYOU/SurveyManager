using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Admins_AdminId",
                schema: "SurveyManager",
                table: "Surveys");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Admins_AdminId",
                schema: "SurveyManager",
                table: "Surveys",
                column: "AdminId",
                principalSchema: "SurveyManager",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Admins_AdminId",
                schema: "SurveyManager",
                table: "Surveys");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Admins_AdminId",
                schema: "SurveyManager",
                table: "Surveys",
                column: "AdminId",
                principalSchema: "SurveyManager",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
