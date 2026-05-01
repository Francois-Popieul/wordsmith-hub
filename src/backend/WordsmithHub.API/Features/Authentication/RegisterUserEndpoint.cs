using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;

namespace WordsmithHub.API.Features.Authentication;

[UsedImplicitly]
public record RegisterUserRequest(
    string? FirstName,
    string? LastName,
    string Email,
    string Password,
    string? PasswordConfirmation);

public class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("Le prénom ne doit pas dépasser 50 caractères.");
        RuleFor(x => x.LastName)
            .MaximumLength(100).WithMessage("Le nom de famille ne doit pas dépasser 100 caractères.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L’adresse email est requise.")
            .EmailAddress().WithMessage("L’adresse email est invalide.")
            .MaximumLength(255).WithMessage("L’adresse email ne doit pas dépasser 255 caractères.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est requis.")
            .MinimumLength(12)
            .WithMessage(
                "Le mot de passe doit contenir au moins 12 caractères incluant majuscules, minuscules, chiffres et caractères spéciaux.")
            .MaximumLength(255).WithMessage("Le mot de passe ne doit pas dépasser 255 caractères.");
        RuleFor(x => x)
            .Must(x => x.Password == x.PasswordConfirmation)
            .WithMessage("Les mots de passe ne correspondent pas.")
            .WithName("PasswordConfirmation");
    }
}

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest, string>
{
    public override void Configure()
    {
        Post("/auth/register");
        Description(x => x.WithTags("authentication"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var result = await command.ExecuteAsync(cancellationToken);

        if (!result.IdentityResult.Succeeded)
        {
            await Send.StringAsync(result.Message!, StatusCodes.Status400BadRequest, "", cancellationToken);
            return;
        }

        await Send.OkAsync(result.Message!, cancellationToken);
    }
}
