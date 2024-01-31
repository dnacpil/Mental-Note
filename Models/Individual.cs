using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class Individual
{
   [Key]
   public int IndividualID { get; set; }
   public string? Name {get; set;}
   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }
}
