using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MentalNote.Models;

namespace MentalNote.Data
{
   public class MentalNoteDbContext : IdentityDbContext
   {
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
    }
    internal static IEnumerable<object> ToList()
        {
            throw new NotImplementedException();
        }
       public MentalNoteDbContext(DbContextOptions<MentalNoteDbContext> options) : base(options) 
       {
       }
       public DbSet<JournalEntry> JournalEntry { get; set; } = null!;
       public DbSet<Notes> Notes { get; set; } = null!;
       public DbSet<MoodRating> MoodRating { get; set; } = null!;
      
   }
}