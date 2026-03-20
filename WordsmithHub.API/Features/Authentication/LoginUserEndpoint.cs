using FastEndpoints;

namespace WordsmithHub.API.Features.Authentication;

public record LoginUserRequest(
    string Email,
    string Password);

public class LoginUserEndpoint(LoginUserHandler handler) : Endpoint<LoginUserRequest, string>
{
    public override void Configure()
    {
        Post("/auth/login");
        Description(x => x.WithTags("authentication")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces<string>(StatusCodes.Status401Unauthorized)
        .Produces<string>(StatusCodes.Status500InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(
            request.Email,
            request.Password
        );

        var result = await handler.HandleAsync(command);

        if (!result.Succeeded)
        {
            await Send.StringAsync(result.Error!, StatusCodes.Status401Unauthorized, "", cancellationToken);
            return;
        }

        await Send.OkAsync(result.Token!, cancellationToken);
    }
}