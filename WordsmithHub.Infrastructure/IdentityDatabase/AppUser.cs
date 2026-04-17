using Microsoft.AspNetCore.Identity;

namespace WordsmithHub.Infrastructure.IdentityDatabase;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
