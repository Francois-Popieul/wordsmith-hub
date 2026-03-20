using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.Authentication;

public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password);

public sealed record RegisterUserResult(IdentityResult IdentityResult, string? Message);

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}

public class RegisterUserHandler(
    IValidator<RegisterUserCommand> validator,
    UserManager<AppUser> userManager)
{
    public async Task<RegisterUserResult> HandleAsync(RegisterUserCommand command)
    {
        await validator.ValidateAndThrowAsync(command);

        var user = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserName = command.Email,
            Email = command.Email
        };

        var result = await userManager.CreateAsync(user, command.Password);

        return !result.Succeeded ?
            new RegisterUserResult(result, result.Errors.First().Description) :
            new RegisterUserResult(IdentityResult.Success, "Inscription réussie. Vérifiez votre messagerie pour confirmer votre compte.");
    }
}
