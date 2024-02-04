using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class JournalEntry
{
    [Key]
    public int JournalEntryID { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    [DataType(DataType.Date)]
    public DateTime? EntryDate { get; set; } = DateTime.Now;
    public string? Title { get; set; }
    [Required]
    public string? JournalContent { get; set; }
    public IdentityUser? Owner { get; set; }
    public string? OwnerId { get; set; }

}