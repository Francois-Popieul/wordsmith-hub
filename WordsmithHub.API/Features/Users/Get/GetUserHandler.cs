using Microsoft.AspNetCore.Identity;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Users.Models;
using WordsmithHub.API.Features.Users.Services;

namespace WordsmithHub.API.Features.Users.Get;

public class GetUserHandler(UserManager<Infrastructure.IdentityDatabase.AppUser> userManager)
{
    public async Task<OperationResult<AppUserDto>> HandleAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return OperationResult.NotFound<AppUserDto>();
        }

        var userDto = user.ToDto();
        {
            return OperationResult.Success(userDto);
        }
    }
}
