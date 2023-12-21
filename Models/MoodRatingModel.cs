using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class MoodRating
{
   [Key]
   public int MoodRatingID {get; set;}
   [DataType(DataType.Date)]
   public DateTime? RatingDate {get; set;}
   // public string? SelectedEmoji { get; set; }
   public int? Rating {get; set;}
   public string? MoodNote {get; set;}
   public string? Weather {get; set;}
   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }
}