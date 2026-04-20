using Microsoft.AspNetCore.Identity;
using WordsmithHub.API.Services.TokenService;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.Authentication;

public record LoginUserCommand(string Email, string Password);

public sealed record LoginResult(bool Succeeded, string? Token);

public class LoginUserHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService)
{
    public async Task<LoginResult> HandleAsync(LoginUserCommand command)
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
