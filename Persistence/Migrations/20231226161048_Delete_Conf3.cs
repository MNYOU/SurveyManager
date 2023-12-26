using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConf3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer",
                column: "SurveyId",
                principalSchema: "SurveyManager",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer",
                column: "SurveyId",
                principalSchema: "SurveyManager",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
