using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.LegalStatusTypes.GetAll;

public class GetAllLegalStatusTypesEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<LegalStatusType>>
{
    public override void Configure()
    {
        Get("/legal-status-types");
        Roles("user", "admin");
        Description(x => x.WithTags("legal-status-types")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllLegalStatusTypesCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
