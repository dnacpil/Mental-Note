using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalNote.Migrations
{
    /// <inheritdoc />
    public partial class ShareProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SharedWithEmail",
                table: "Notes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedWithEmail",
                table: "Notes");
        }
    }
}
