using FastEndpoints;
using FluentValidation;

namespace WordsmithHub.API.Features.Authentication;

public abstract record RegisterUserRequest(string FirstName, string LastName, string Email, string Password);

public class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}

public class RegisterUserEndpoint(RegisterUserHandler handler) : Endpoint<RegisterUserRequest, string>
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

        var result = await handler.HandleAsync(command);

        if (!result.IdentityResult.Succeeded)
        {
            await Send.StringAsync(result.Message!, StatusCodes.Status400BadRequest, "", cancellationToken);
            return;
        }

        await Send.OkAsync(result.Message!, cancellationToken);
    }
}
