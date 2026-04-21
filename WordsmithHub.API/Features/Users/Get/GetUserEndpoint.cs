using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Users.Models;

namespace WordsmithHub.API.Features.Users.Get;

public class GetUserEndpoint(GetUserHandler handler) : EndpointWithoutRequest<AppUserDto>
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

        var userId = Route<Guid>("userId");

        var result = await handler.HandleAsync(userId);

        switch (result.Status)
        {
            case OperationStatus.NotFound:
                await Send.NotFoundAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value!, cancellationToken);
                return;
        }
    }
}
