using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.Rates.Delete;

public class DeleteRateEndpoint : ApiEndpointWithoutRequest<NoContent>
{
    public override void Configure()
    {
        Delete("/rate/{rateId:guid}");
        Roles("user");
        Description(x => x.WithTags("rate")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var rateId = Route<Guid>("rateId");

        var command = new DeleteRateCommand(appUserId, rateId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
