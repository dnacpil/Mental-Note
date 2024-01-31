using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Models;
public class Provider
{
   [Key]
   public int ProviderID { get; set; }
   public string? Name {get; set;}
   public IdentityUser? Owner { get; set; }
   public string? OwnerId { get; set; }
}
