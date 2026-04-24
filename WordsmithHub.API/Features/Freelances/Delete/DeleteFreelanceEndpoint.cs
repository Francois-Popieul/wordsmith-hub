using FastEndpoints;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.Freelances.Delete;

public class DeleteFreelanceEndpoint : EndpointWithoutRequest<Guid>
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
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new DeleteFreelanceCommand(appUserId, freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

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
