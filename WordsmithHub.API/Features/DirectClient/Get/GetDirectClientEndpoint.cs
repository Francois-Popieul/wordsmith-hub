using System.Security.Claims;
using FastEndpoints;

namespace WordsmithHub.API.Features.DirectClient.Get;

public class GetDirectClientEndpoint(GetDirectClientHandler handler) : EndpointWithoutRequest<Domain.DirectClient>
{
    public override void Configure()
    {
        Get("/directclient/{directClientId:guid}");
        Roles("user, admin");
        Description(x => x.WithTags("directclient")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var tokenUserId = User.FindFirstValue("sub");

        if (tokenUserId == null)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var command = new GetDirectClientCommand(DirectClientId: Route<Guid>("directClientId"));

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result == null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        if (result.UserId != Guid.Parse(tokenUserId))
        {
            await Send.ForbiddenAsync(cancellationToken);
            return;
        }

        await Send.OkAsync(result, cancellationToken);
    }
}
