using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Rates.Models;

namespace WordsmithHub.API.Features.Rates.GetAll;

public class GetAllRatesEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<RateDto>>
{
    public override void Configure()
    {
        Get("/rates");
        Roles("user");
        Description(x => x.WithTags("rates")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllRatesCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
