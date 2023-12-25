using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AnalystAccessConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analysts",
                schema: "SurveyManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalystAccess",
                schema: "SurveyManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AnalystId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessKey = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalystAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalystAccess_Analysts_AnalystId",
                        column: x => x.AnalystId,
                        principalSchema: "SurveyManager",
                        principalTable: "Analysts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalystAccess_AnalystId",
                schema: "SurveyManager",
                table: "AnalystAccess",
                column: "AnalystId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalystAccess",
                schema: "SurveyManager");

            migrationBuilder.DropTable(
                name: "Analysts",
                schema: "SurveyManager");
        }
    }
}
