using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SequenceNumber",
                schema: "SurveyManager",
                table: "Questions",
                newName: "Number");

            migrationBuilder.AddColumn<bool>(
                name: "ContainsDefaultQuestions",
                schema: "SurveyManager",
                table: "Surveys",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                schema: "SurveyManager",
                table: "Questions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "SurveyManager",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PatientFIO",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainsDefaultQuestions",
                schema: "SurveyManager",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "SurveyManager",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Number",
                schema: "SurveyManager",
                table: "Questions",
                newName: "SequenceNumber");

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                schema: "SurveyManager",
                table: "Questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientFIO",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
