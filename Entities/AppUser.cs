using Microsoft.AspNetCore.Identity;

namespace WebApiAdvance.Entities;

public class AppUser:IdentityUser
{
    public string Fullname { get; set; }
}
