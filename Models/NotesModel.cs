using System.ComponentModel.DataAnnotations;

namespace MentalNote.Models;
public class Notes
{
   [Key]
   public int NoteID {get; set;}
   public DateTime? NoteDate {get; set;}
   public string? Title {get; set;}
   public string? Note {get; set;}
   public string? Exercises {get; set;}
}