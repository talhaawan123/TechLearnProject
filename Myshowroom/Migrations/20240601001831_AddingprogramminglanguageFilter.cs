using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myshowroom.Migrations
{
    /// <inheritdoc />
    public partial class AddingprogramminglanguageFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguageId",
                table: "LearningNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningNotes_ProgrammingLanguageId",
                table: "LearningNotes",
                column: "ProgrammingLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningNotes_ProgrammingLanguages_ProgrammingLanguageId",
                table: "LearningNotes",
                column: "ProgrammingLanguageId",
                principalTable: "ProgrammingLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningNotes_ProgrammingLanguages_ProgrammingLanguageId",
                table: "LearningNotes");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropIndex(
                name: "IX_LearningNotes_ProgrammingLanguageId",
                table: "LearningNotes");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguageId",
                table: "LearningNotes");
        }
    }
}
