using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineNotebook.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classes_StudyClassId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StudyClassId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StudyClassId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StudentClases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentClases_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentClases_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentClases_ClassId",
                table: "StudentClases",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClases_StudentId",
                table: "StudentClases",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentClases");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "StudyClassId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudyClassId",
                table: "Users",
                column: "StudyClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_StudyClassId",
                table: "Users",
                column: "StudyClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }
    }
}
