using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Domains.User.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddIsInitialUserToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInitialUser",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInitialUser",
                table: "Users");
        }
    }
}
