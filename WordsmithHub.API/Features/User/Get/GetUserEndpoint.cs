using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.User.Get;

public class GetUserEndpoint(GetUserHandler handler) : EndpointWithoutRequest<AppUser>
{
    public override void Configure()
    {
        Get("/user/{userId:guid}");
        Roles("User", "Admin");
        Description(x => x.WithTags("user")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var tokenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (tokenUserId == null)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        if (tokenUserId != Route<Guid>("userId").ToString())
        {
            await Send.ForbiddenAsync(cancellationToken);
            return;
        }

        var command = new GetUserCommand(UserId: Route<Guid>("userId"));

        var result = await handler.HandleAsync(command);

        if (result == null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        await Send.OkAsync(result, cancellationToken);
    }
}
