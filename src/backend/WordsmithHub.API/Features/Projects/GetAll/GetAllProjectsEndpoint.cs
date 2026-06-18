using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Projects.Models;

namespace WordsmithHub.API.Features.Projects.GetAll;

public class GetAllProjectsEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<ProjectDto>>
{
    public override void Configure()
    {
        Get("/projects");
        Roles("user");
        Description(x => x.WithTags("projects")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllProjectsCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
