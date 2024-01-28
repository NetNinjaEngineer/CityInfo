using Microsoft.AspNetCore.Identity;

namespace CityInfo.MVC.Models;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}
