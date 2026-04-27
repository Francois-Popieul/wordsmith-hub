using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.DirectCustomers.Models;

namespace WordsmithHub.API.Features.DirectCustomers.GetAll;

public class GetAllDirectCustomersEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<DirectCustomerDto>>
{
    public override void Configure()
    {
        Get("/directcustomers");
        Roles("user", "admin");
        Description(x => x.WithTags("directcustomer")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllDirectCustomersCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}