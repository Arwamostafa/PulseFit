using Microsoft.AspNetCore.Identity;

namespace PulseFit.DAL.Entities;

public class AppUser : IdentityUser
{

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

}

