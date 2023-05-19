using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTraining.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotepadTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTraining",
                table: "Notepads",
                newName: "DateNote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateNote",
                table: "Notepads",
                newName: "DateTraining");
        }
    }
}
