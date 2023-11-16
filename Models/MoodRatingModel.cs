using System.ComponentModel.DataAnnotations;

namespace MentalNote.Models;
public class MoodRatingModel
{
   [Key]
   public int MoodRatingID {get; set;}
   public DateTime? RatingDate {get; set;}
   public int? MoodRating {get; set;}
   public string? MoodNote {get; set;}
   public string? Weather {get; set;}
}