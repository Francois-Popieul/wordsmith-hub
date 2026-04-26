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
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(12).MaximumLength(255);
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

        var response = new AccessTokenResponse()
        {
            AccessToken = result.Token!,
            RefreshToken = "refresh",
            ExpiresIn = jwtSection.GetValue<int>("AccessTokenSeconds")
        };

        await Send.OkAsync(response, cancellationToken);
    }
}
