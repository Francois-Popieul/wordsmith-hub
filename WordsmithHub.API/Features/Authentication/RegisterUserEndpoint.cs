using FastEndpoints;

namespace WordsmithHub.API.Features.Authentication;

public abstract record RegisterUserRequest(string FirstName, string LastName, string Email, string Password);

public class RegisterUserEndpoint(RegisterUserHandler handler) : Endpoint<RegisterUserRequest, string>
{
    public override void Configure()
    {
        Post("/auth/register");
        Description(x => x.WithTags("authentication")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest));
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
