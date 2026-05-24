using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.Projects.Delete;

public class DeleteProjectEndpoint : ApiEndpointWithoutRequest<NoContent>
{
    public override void Configure()
    {
        Delete("/project/{projectId:guid}");
        Roles("user");
        Description(x => x.WithTags("project")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var projectId = Route<Guid>("projectId");

        var command = new DeleteProjectCommand(appUserId, projectId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
