using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalNote.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToMoodRatingAndJournalEntryModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "JournalEntry");

            migrationBuilder.RenameColumn(
                name: "Weather",
                table: "MoodRating",
                newName: "Emoji");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "MoodRating",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emoji",
                table: "MoodRating",
                newName: "Weather");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "MoodRating",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "JournalEntry",
                type: "TEXT",
                nullable: true);
        }
    }
}
