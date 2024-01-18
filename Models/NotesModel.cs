using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class Notes
{
   [Key]
   public int NoteID { get; set; }
   [DataType(DataType.Date)]
   //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
   public DateTime? NoteDate { get; set; } = DateTime.Now;
   public string? Title { get; set; }
   public string? Note { get; set; }
   public string? Exercises { get; set; }
   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }
}