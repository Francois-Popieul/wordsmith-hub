using WordsmithHub.API.Features.Users.Models;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.Users.Services;

public static class AppUserExtensions
{
    public static AppUserDto ToDto(this AppUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new AppUserDto
        {
            Id = Guid.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber
        };
    }
}
