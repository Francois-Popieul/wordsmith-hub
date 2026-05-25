using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Statuses.GetAll;

public class GetAllWorkOrderStatusesEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<Status>>
{
    public override void Configure()
    {
        Get("/workorder-statuses");
        Roles("user", "admin");
        Description(x => x.WithTags("statuses")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllProjectStatusesCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
