using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class JournalEntry
{
    [Key]
   public int JournalEntryID {get; set;}
   [DataType(DataType.Date)]
   public DateTime? EntryDate {get; set;}
   public string? Title {get; set;}
   public string? JournalContent {get; set;}
   public string? Tags {get; set;}//Remove this and replace these with goals instead, but see if goals will need to be a table/model of its own
   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }

}