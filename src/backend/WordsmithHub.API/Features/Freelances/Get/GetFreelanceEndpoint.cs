using FastEndpoints;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;

namespace WordsmithHub.API.Features.Freelances.Get;

public class GetFreelanceEndpoint : EndpointWithoutRequest<FreelanceDto>
{
    public override void Configure()
    {
        Get("/freelance/{freelanceId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new GetFreelanceCommand(appUserId, freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        switch (result.Status)
        {
            case OperationStatus.Forbidden:
                await Send.ForbiddenAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value!, cancellationToken);
                return;
        }
    }
}
