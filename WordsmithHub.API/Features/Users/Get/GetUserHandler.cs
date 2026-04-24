using FastEndpoints;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Users.Models;
using WordsmithHub.API.Features.Users.Services;

namespace WordsmithHub.API.Features.Users.Get;

public record GetUserCommand(Guid AppUserId) : ICommand<OperationResult<AppUserDto>>;

public class GetUserHandler(UserManager<Infrastructure.IdentityDatabase.AppUser> userManager)
    : ICommandHandler<GetUserCommand, OperationResult<AppUserDto>>
{
    public async Task<OperationResult<AppUserDto>> ExecuteAsync(GetUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(command.AppUserId.ToString());

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
