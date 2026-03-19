using FastEndpoints;
using FluentValidation;

namespace WordsmithHub.API.Features;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);

public record RegisterUserResponse(string Message);

public class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class RegisterUserEndpoint(RegisterUserHandler handler) : Endpoint<RegisterUserRequest, RegisterUserResponse>
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

        if (!result.Succeeded)
            throw new Exception("Failed to register user");

        await Send.OkAsync(new RegisterUserResponse("Registration successful. Please check your email to confirm your account."), cancellationToken);
    }
}