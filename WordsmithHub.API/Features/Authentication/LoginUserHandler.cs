using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WordsmithHub.API.Services;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.Authentication;

public record LoginUserCommand(string Email, string Password);

public sealed record LoginResult(bool Succeeded, string? Token, string? Error);

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}

public class LoginUserHandler(
    IValidator<LoginUserCommand> validator,
    UserManager<AppUser> userManager,
    ITokenService tokenService)
{
    public async Task<LoginResult> HandleAsync(LoginUserCommand command)
    {
        await validator.ValidateAndThrowAsync(command);

        var user = await userManager.FindByEmailAsync(command.Email);
        if (user == null)
            return new LoginResult(false, null, "Identifiants incorrects.");

        var result = await userManager.CheckPasswordAsync(user, command.Password);

        if (!result)
            return new LoginResult(false, null, "Identifiants incorrects.");

        var token = await tokenService.CreateAccessTokenAsync(user);

        return new LoginResult(true, token, null);
    }
}