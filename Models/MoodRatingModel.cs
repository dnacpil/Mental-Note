using System.ComponentModel.DataAnnotations;

namespace MentalNote.Models;
public class MoodRating
{
   [Key]
   public int MoodRatingID {get; set;}
   public DateTime? RatingDate {get; set;}
   public int? Rating {get; set;}
   public string? MoodNote {get; set;}
   public string? Weather {get; set;}
}