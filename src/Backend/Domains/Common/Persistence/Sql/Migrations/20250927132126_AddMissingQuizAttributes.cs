using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Domains.Common.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingQuizAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizState",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswered",
                table: "Questions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizState",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsAnswered",
                table: "Questions");
        }
    }
}
