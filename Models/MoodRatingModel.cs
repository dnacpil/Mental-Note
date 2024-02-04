using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class MoodRating
{
   [Key]
   public int MoodRatingID { get; set; }
   [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
   [DataType(DataType.Date)]
   public DateTime Date { get; set; } = DateTime.Now;
   //public string? Emoji { get; set; }
   [Required]
   [Range(1,10,ErrorMessage ="You can only select from 1 to 10.")]
   public int Rating { get; set; }
   public string? MoodNote { get; set; }

   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }
}