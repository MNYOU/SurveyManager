using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConf2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer");

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
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer",
                column: "SurveyId",
                principalSchema: "SurveyManager",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer");

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
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer",
                column: "SurveyId",
                principalSchema: "SurveyManager",
                principalTable: "Surveys",
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
    }
}
