using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.LegalStatuses.Models;

namespace WordsmithHub.API.Features.LegalStatuses.GetAll;

public class GetAllLegalStatusesEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<LegalStatusDto>>
{
    public override void Configure()
    {
        Get("/legalstatuses");
        Roles("user");
        Description(x => x.WithTags("legalstatus")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllLegalStatusesCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}