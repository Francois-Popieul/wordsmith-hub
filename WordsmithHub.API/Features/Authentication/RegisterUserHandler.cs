using Microsoft.AspNetCore.Identity;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.Authentication;

public record RegisterUserCommand(
    string? FirstName,
    string? LastName,
    string Email,
    string Password);

public sealed record RegisterUserResult(IdentityResult IdentityResult, string? Message);

public class RegisterUserHandler(UserManager<AppUser> userManager)
{
    public async Task<RegisterUserResult> HandleAsync(RegisterUserCommand command)
    {
        var user = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = command.Email,
            Email = command.Email
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return new RegisterUserResult(result, result.Errors.First().Description);
        }

        await userManager.AddToRoleAsync(user, "User");

        return new RegisterUserResult(IdentityResult.Success,
            "Inscription réussie. Vérifiez votre messagerie pour confirmer votre compte.");
    }
}
