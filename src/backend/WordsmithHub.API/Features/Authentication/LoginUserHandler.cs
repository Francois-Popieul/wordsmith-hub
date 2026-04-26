using FastEndpoints;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using WordsmithHub.API.Services.TokenService;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.Authentication;

public record LoginUserCommand(string Email, string Password) : ICommand<LoginResult>;

public sealed record LoginResult(bool Succeeded, string? Token);

[UsedImplicitly]
public class LoginUserHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService)
    : ICommandHandler<LoginUserCommand, LoginResult>
{
    public async Task<LoginResult> ExecuteAsync(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, command.Password))
        {
            return new LoginResult(false, null);
        }

        var token = await tokenService.CreateAccessTokenAsync(user);

        return new LoginResult(true, token);
    }
}
