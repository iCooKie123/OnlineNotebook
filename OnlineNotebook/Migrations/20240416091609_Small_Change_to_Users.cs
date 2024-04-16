using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineNotebook.Migrations
{
    /// <inheritdoc />
    public partial class Small_Change_to_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfStudy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfStudy",
                table: "Users");
        }
    }
}
