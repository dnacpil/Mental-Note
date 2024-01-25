using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalNote.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emoji",
                table: "MoodRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Emoji",
                table: "MoodRating",
                type: "TEXT",
                nullable: true);
        }
    }
}
