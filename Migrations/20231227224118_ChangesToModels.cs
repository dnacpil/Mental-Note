using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalNote.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatingDate",
                table: "MoodRating",
                newName: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "MoodRating",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "MoodRating",
                newName: "RatingDate");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "MoodRating",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
