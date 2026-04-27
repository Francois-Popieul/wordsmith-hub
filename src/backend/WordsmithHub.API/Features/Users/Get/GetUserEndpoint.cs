using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Users.Models;

namespace WordsmithHub.API.Features.Users.Get;

public class GetUserEndpoint : ApiEndpointWithoutRequest<AppUserDto>
{
    public override void Configure()
    {
        Get("/user/{userId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("user")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        if (appUserId != Route<Guid>("userId"))
        {
            await Send.ForbiddenAsync(cancellationToken);
            return;
        }

        var command = new GetUserCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
