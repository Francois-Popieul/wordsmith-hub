using FastEndpoints;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Infrastructure.IdentityDatabase;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Features.Authentication;

public record RegisterUserCommand(
    string? FirstName,
    string? LastName,
    string Email,
    string Password) : ICommand<RegisterUserResult>;

public sealed record RegisterUserResult(IdentityResult IdentityResult, string? Message);

[UsedImplicitly]
public class RegisterUserHandler(
    UserManager<AppUser> userManager,
    IFreelanceFactory freelanceFactory,
    MainDbContext dbContext)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> ExecuteAsync(RegisterUserCommand command, CancellationToken cancellationToken)
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

        await userManager.AddToRoleAsync(user, "user");

        var freelance = freelanceFactory.CreateFreelanceProfile(
            Guid.Parse(user.Id),
            user.FirstName,
            user.LastName,
            user.Email);
        await dbContext.Freelances.AddAsync(freelance, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        // TODO
        // Send email confirmation and update result message
        // "Inscription réussie. Vérifiez votre messagerie pour confirmer votre compte."
        return new RegisterUserResult(IdentityResult.Success,
            "Inscription réussie.");
    }
}
