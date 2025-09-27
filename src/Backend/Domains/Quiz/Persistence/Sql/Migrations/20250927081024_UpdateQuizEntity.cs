using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Domains.Common.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarEntityQuiz",
                columns: table => new
                {
                    BarsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    QuizzesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarEntityQuiz", x => new { x.BarsId, x.QuizzesId });
                    table.ForeignKey(
                        name: "FK_BarEntityQuiz_Bars_BarsId",
                        column: x => x.BarsId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarEntityQuiz_Quizzes_QuizzesId",
                        column: x => x.QuizzesId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BarEntityQuiz_QuizzesId",
                table: "BarEntityQuiz",
                column: "QuizzesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarEntityQuiz");
        }
    }
}
