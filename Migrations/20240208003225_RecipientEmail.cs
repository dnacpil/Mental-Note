using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalNote.Migrations
{
    /// <inheritdoc />
    public partial class RecipientEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SharedWithEmail",
                table: "Notes",
                newName: "RecipientEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipientEmail",
                table: "Notes",
                newName: "SharedWithEmail");
        }
    }
}
