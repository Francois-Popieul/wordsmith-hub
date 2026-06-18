using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Rates.Models;

namespace WordsmithHub.API.Features.Rates.GetAll;

public class GetAllRatesByCustomerIdEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<RateDto>>
{
    public override void Configure()
    {
        Get("/rates/directcustomer/{directCustomerId}");
        Roles("user");
        Description(x => x.WithTags("rates")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var directCustomerId = Route<Guid>("directCustomerId");

        var command = new GetAllRatesByCustomerIdCommand(appUserId, directCustomerId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}