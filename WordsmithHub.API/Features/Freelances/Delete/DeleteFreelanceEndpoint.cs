using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.Freelances.Delete;

public class DeleteFreelanceEndpoint(DeleteFreelanceHandler handler) : EndpointWithoutRequest<Guid>
{
    public override void Configure()
    {
        Delete("/freelance/{freelanceId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = User.FindFirstValue("sub");

        if (appUserId == null)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var freelanceId = Route<Guid>("freelanceId");

        var result = await handler.HandleAsync(Guid.Parse(appUserId), freelanceId, cancellationToken);

        switch (result.Status)
        {
            case OperationStatus.Forbidden:
                await Send.ForbiddenAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value, cancellationToken);
                return;
        }
    }
}
