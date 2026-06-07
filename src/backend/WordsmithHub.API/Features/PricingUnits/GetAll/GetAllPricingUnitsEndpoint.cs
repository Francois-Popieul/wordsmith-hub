using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.PricingUnits.GetAll;

public class GetAllPricingUnitsEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<PricingUnit>>
{
    public override void Configure()
    {
        Get("/pricing-units");
        Roles("user", "admin");
        Description(x => x.WithTags("pricing-units")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllPricingUnitsCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}