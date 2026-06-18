using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Projects.Update;

[UsedImplicitly]
public record UpdateProjectStatusRequest(
    int StatusId
);

public class UpdateProjectStatusRequestValidator : Validator<UpdateProjectStatusRequest>
{
    public UpdateProjectStatusRequestValidator()
    {
        RuleFor(x => x.StatusId)
            .NotEmpty()
            .InclusiveBetween(StatusIds.Project.InProgress, StatusIds.Project.Completed);
    }
}

public class UpdateProjectStatusEndpoint : ApiEndpoint<UpdateProjectStatusRequest, Guid>
{
    public override void Configure()
    {
        Put("/project/{projectId:guid}/status");
        Roles("user");
        Description(x => x.WithTags("project")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateProjectStatusRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var projectId = Route<Guid>("projectId");

        var command = new UpdateProjectStatusCommand(
            appUserId,
            projectId,
            request.StatusId
        );

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
