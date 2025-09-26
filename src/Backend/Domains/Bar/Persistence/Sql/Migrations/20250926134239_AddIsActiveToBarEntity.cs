using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Domains.Bar.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToBarEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Bars",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Bars");
        }
    }
}
