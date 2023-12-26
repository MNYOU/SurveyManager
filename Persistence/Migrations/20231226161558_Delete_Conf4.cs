﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConf4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientSurveyAnswer_Surveys_SurveyId",
                schema: "SurveyManager",
                table: "PatientSurveyAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                schema: "SurveyManager",
                table: "PatientAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
    }
}
