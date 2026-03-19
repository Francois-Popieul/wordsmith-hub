using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WordsmithHub.Infrastructure;

namespace WordsmithHub.API.Features;

public record RegisterUserCommand (
    string FirstName,
    string LastName,
    string Email,
    string Password);

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class RegisterUserHandler(
    IValidator<RegisterUserCommand> validator,
    UserManager<AppUser> userManager)
{
    public async Task<IdentityResult> HandleAsync(RegisterUserCommand command)
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
        if (!result.Succeeded)
            return result;

        return IdentityResult.Success;
    }
}
