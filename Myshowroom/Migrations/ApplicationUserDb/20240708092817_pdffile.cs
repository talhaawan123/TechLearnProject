using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Myshowroom.Migrations.ApplicationUserDb
{
    /// <inheritdoc />
    public partial class pdffile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResumeFilePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeFilePath",
                table: "AspNetUsers");
        }
    }
}
