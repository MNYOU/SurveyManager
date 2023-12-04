using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_PatientAnswers_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.DropIndex(
                name: "IX_AnswerOption_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.DropColumn(
                name: "PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "SurveyManager",
                table: "Surveys",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "AnswerOptionPatientAnswer",
                schema: "SurveyManager",
                columns: table => new
                {
                    PatientAnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedAnswerOptionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptionPatientAnswer", x => new { x.PatientAnswerId, x.SelectedAnswerOptionsId });
                    table.ForeignKey(
                        name: "FK_AnswerOptionPatientAnswer_AnswerOption_SelectedAnswerOption~",
                        column: x => x.SelectedAnswerOptionsId,
                        principalSchema: "SurveyManager",
                        principalTable: "AnswerOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerOptionPatientAnswer_PatientAnswers_PatientAnswerId",
                        column: x => x.PatientAnswerId,
                        principalSchema: "SurveyManager",
                        principalTable: "PatientAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptionPatientAnswer_SelectedAnswerOptionsId",
                schema: "SurveyManager",
                table: "AnswerOptionPatientAnswer",
                column: "SelectedAnswerOptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerOptionPatientAnswer",
                schema: "SurveyManager");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                schema: "SurveyManager",
                table: "Surveys",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOption_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "PatientAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_PatientAnswers_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "PatientAnswerId",
                principalSchema: "SurveyManager",
                principalTable: "PatientAnswers",
                principalColumn: "Id");
        }
    }
}
