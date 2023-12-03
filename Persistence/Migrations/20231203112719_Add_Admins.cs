using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_PatientAnswer_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_Question_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswer_Question_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Survey_SurveyId",
                schema: "SurveyManager",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_users_AdminId",
                schema: "SurveyManager",
                table: "Survey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Survey",
                schema: "SurveyManager",
                table: "Survey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                schema: "SurveyManager",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientAnswer",
                schema: "SurveyManager",
                table: "PatientAnswer");

            migrationBuilder.RenameTable(
                name: "Survey",
                schema: "SurveyManager",
                newName: "Surveys",
                newSchema: "SurveyManager");

            migrationBuilder.RenameTable(
                name: "Question",
                schema: "SurveyManager",
                newName: "Questions",
                newSchema: "SurveyManager");

            migrationBuilder.RenameTable(
                name: "PatientAnswer",
                schema: "SurveyManager",
                newName: "PatientAnswers",
                newSchema: "SurveyManager");

            migrationBuilder.RenameIndex(
                name: "IX_Survey_AdminId",
                schema: "SurveyManager",
                table: "Surveys",
                newName: "IX_Surveys_AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_SurveyId",
                schema: "SurveyManager",
                table: "Questions",
                newName: "IX_Questions_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAnswer_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                newName: "IX_PatientAnswers_QuestionId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                schema: "SurveyManager",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                schema: "SurveyManager",
                table: "Surveys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                schema: "SurveyManager",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientAnswers",
                schema: "SurveyManager",
                table: "PatientAnswers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Admins",
                schema: "SurveyManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessKey = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AccessKey",
                schema: "SurveyManager",
                table: "Admins",
                column: "AccessKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_PatientAnswers_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "PatientAnswerId",
                principalSchema: "SurveyManager",
                principalTable: "PatientAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_Questions_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Questions_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "Questions",
                column: "SurveyId",
                principalSchema: "SurveyManager",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_users_AdminId",
                schema: "SurveyManager",
                table: "Surveys",
                column: "AdminId",
                principalSchema: "SurveyManager",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_PatientAnswers_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOption_Questions_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAnswers_Questions_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_users_AdminId",
                schema: "SurveyManager",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "Admins",
                schema: "SurveyManager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                schema: "SurveyManager",
                table: "Surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                schema: "SurveyManager",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientAnswers",
                schema: "SurveyManager",
                table: "PatientAnswers");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                schema: "SurveyManager",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Surveys",
                schema: "SurveyManager",
                newName: "Survey",
                newSchema: "SurveyManager");

            migrationBuilder.RenameTable(
                name: "Questions",
                schema: "SurveyManager",
                newName: "Question",
                newSchema: "SurveyManager");

            migrationBuilder.RenameTable(
                name: "PatientAnswers",
                schema: "SurveyManager",
                newName: "PatientAnswer",
                newSchema: "SurveyManager");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_AdminId",
                schema: "SurveyManager",
                table: "Survey",
                newName: "IX_Survey_AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SurveyId",
                schema: "SurveyManager",
                table: "Question",
                newName: "IX_Question_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAnswers_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswer",
                newName: "IX_PatientAnswer_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Survey",
                schema: "SurveyManager",
                table: "Survey",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                schema: "SurveyManager",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientAnswer",
                schema: "SurveyManager",
                table: "PatientAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_PatientAnswer_PatientAnswerId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "PatientAnswerId",
                principalSchema: "SurveyManager",
                principalTable: "PatientAnswer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOption_Question_QuestionId",
                schema: "SurveyManager",
                table: "AnswerOption",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAnswer_Question_QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswer",
                column: "QuestionId",
                principalSchema: "SurveyManager",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Survey_SurveyId",
                schema: "SurveyManager",
                table: "Question",
                column: "SurveyId",
                principalSchema: "SurveyManager",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_users_AdminId",
                schema: "SurveyManager",
                table: "Survey",
                column: "AdminId",
                principalSchema: "SurveyManager",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
