using Microsoft.AspNetCore.Identity;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.User.Get;

public record GetUserCommand(Guid UserId);

public class GetUserHandler(UserManager<AppUser> userManager)
{
    public async Task<AppUser?> HandleAsync(GetUserCommand command)
    {
        var user = await userManager.FindByIdAsync(command.UserId.ToString());

        return user ?? null;
    }
}
