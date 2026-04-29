using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace WordsmithHub.API.Features.Authentication;

[UsedImplicitly]
public record LoginUserRequest(string Email, string Password);

public class LoginUserRequestValidator : Validator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'adresse email est requise.")
            .EmailAddress().WithMessage("L'adresse email est invalide.")
            .MaximumLength(255).WithMessage("L'adresse email ne doit pas dépasser 255 caractères.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est requis.")
            .MaximumLength(255).WithMessage("Le mot de passe ne doit pas dépasser 255 caractères.");
    }
}

public class LoginUserEndpoint(IConfiguration configuration) : Endpoint<LoginUserRequest, AccessTokenResponse>
{
    public override void Configure()
    {
        Post("/auth/login");
        Description(x => x.WithTags("authentication")
            .Produces(StatusCodes.Status401Unauthorized));
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await command.ExecuteAsync(cancellationToken);

        if (!result.Succeeded)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var jwtSection = configuration.GetSection("Jwt");

        var response = new AccessTokenResponse
        {
            AccessToken = result.Token!,
            RefreshToken = "refresh",
            ExpiresIn = jwtSection.GetValue<int>("AccessTokenSeconds")
        };

        await Send.OkAsync(response, cancellationToken);
    }
}
