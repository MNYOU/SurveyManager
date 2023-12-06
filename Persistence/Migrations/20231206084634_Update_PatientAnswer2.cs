using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientAnswer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientFIO",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyAnswerId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PatientSurveyAnswer",
                schema: "SurveyManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSurveyAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalSchema: "SurveyManager",
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientAnswers_SurveyAnswerId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                column: "SurveyAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSurveyAnswer_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAnswers_PatientSurveyAnswer_SurveyAnswerId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                column: "SurveyAnswerId",
                principalSchema: "SurveyManager",
                principalTable: "PatientSurveyAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_PatientSurveyAnswer_SurveyAnswerId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropTable(
                name: "PatientSurveyAnswer",
                schema: "SurveyManager");

            migrationBuilder.DropIndex(
                name: "IX_PatientAnswers_SurveyAnswerId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropColumn(
                name: "SurveyAnswerId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.AddColumn<string>(
                name: "PatientFIO",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "uuid",
                nullable: true);
        }
    }
}
