using Microsoft.EntityFrameworkCore;
using MentalNote.Models;
namespace MentalNote.Data
{
   public class MentalNoteDbContext : DbContext
   {
       public MentalNoteDbContext(DbContextOptions<MentalNoteDbContext> options) :
base(options)
{}
       public DbSet<JournalEntry> JournalEntry { get; set; } = null!;
       public DbSet<Notes> Notes { get; set; } = null!;

       public DbSet<MoodRating> MoodRating { get; set; } = null!;
      
   }
}