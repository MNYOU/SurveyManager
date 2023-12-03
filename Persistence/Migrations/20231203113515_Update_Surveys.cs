using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_users_AdminId",
                schema: "SurveyManager",
                table: "Surveys");

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
                name: "FK_Surveys_Admins_AdminId",
                schema: "SurveyManager",
                table: "Surveys");

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
    }
}
