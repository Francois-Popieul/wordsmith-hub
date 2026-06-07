using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.Projects.Add;

[UsedImplicitly]
public record AddProjectRequest(
    string Name,
    string Domain,
    string? Description,
    Guid[] DirectCustomerIds,
    string? EndCustomerName);

public class AddProjectRequestValidator : Validator<AddProjectRequest>
{
    public AddProjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);
        RuleFor(x => x.Domain)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Description)
            .MaximumLength(1000);
        RuleFor(x => x.DirectCustomerIds)
            .NotEmpty();
    }
}

public class AddProjectEndpoint : ApiEndpoint<AddProjectRequest, Guid>
{
    public override void Configure()
    {
        Post("/project");
        Roles("user");
        Description(x => x.WithTags("project")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(AddProjectRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new AddProjectCommand(
            request.Name,
            request.Domain,
            request.Description ?? string.Empty,
            request.DirectCustomerIds,
            request.EndCustomerName ?? string.Empty,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
