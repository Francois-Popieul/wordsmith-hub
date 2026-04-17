using Microsoft.AspNetCore.Identity;
using WordsmithHub.API.Features.Users.Services;
using WordsmithHub.API.Features.Users.Model;

namespace WordsmithHub.API.Features.Users.Get;

public record GetUserCommand(Guid UserId);

public class GetUserHandler(UserManager<Infrastructure.IdentityDatabase.AppUser> userManager)
{
    public async Task<AppUserDto?> HandleAsync(GetUserCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());

        if (user == null)
        {
            return null;
        }

        var userDto = user.ToDto();
        {
            return userDto;
        }
    }
}
