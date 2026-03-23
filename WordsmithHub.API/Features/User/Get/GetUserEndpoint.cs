using FastEndpoints;
using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Features.User.Get;

public class GetUserEndpoint(GetUserHandler handler) : EndpointWithoutRequest<AppUser>
{
    public override void Configure()
    {
        Get("/user/{userId:guid}");
        Description(x => x.WithTags("user")
            .Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
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
