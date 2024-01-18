using Microsoft.AspNetCore.Identity;

namespace MentalNote.Areas.Identity.Data;

public class MentalNoteUser : IdentityUser
{
    
    [PersonalData]
    public string? UserType { get; set; }

}