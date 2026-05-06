using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.Freelances.Update;

[UsedImplicitly]
public record UpdateFreelanceServicesRequest(IReadOnlyList<int> ServiceIds);

public class UpdateFreelanceServicesRequestValidator : Validator<UpdateFreelanceServicesRequest>
{
    public UpdateFreelanceServicesRequestValidator()
    {
        RuleFor(x => x.ServiceIds).NotEmpty().Must(ids => ids.Count is > 0 and <= 25);
    }
}

public class UpdateFreelanceServicesEndpoint : ApiEndpoint<UpdateFreelanceServicesRequest, Guid>
{
    public override void Configure()
    {
        Put("/freelance/{freelanceId:guid}/services");
        Roles("user");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateFreelanceServicesRequest request,
        CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new UpdateFreelanceServicesCommand(
            request.ServiceIds,
            appUserId,
            freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
