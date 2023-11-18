using System.ComponentModel.DataAnnotations;

namespace MentalNote.Models;
public class JournalEntry
{
    [Key]
   public int JournalEntryID {get; set;}
   public DateTime? EntryDate {get; set;}
   public string? Title {get; set;}
   public string? JournalContent {get; set;}
   public string? Tags {get; set;}

}