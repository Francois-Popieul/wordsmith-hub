using FastEndpoints;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.DirectCustomers.Delete;

public class DeleteDirectCustomerEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/directcustomer/{directCustomerId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("directcustomer")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var directCustomer = Route<Guid>("directCustomerId");

        var command = new DeleteDirectCustomerCommand(appUserId, directCustomer);

        var result = await command.ExecuteAsync(cancellationToken);

        switch (result.Status)
        {
            case OperationStatus.Forbidden:
                await Send.ForbiddenAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value, cancellationToken);
                return;
        }
    }
}
