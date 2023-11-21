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
   public string? Tags {get; set;}
   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }

}