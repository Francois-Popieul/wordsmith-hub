using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.DirectCustomers.Delete;

public class DeleteDirectCustomerEndpoint(DeleteDirectCustomerHandler handler) : EndpointWithoutRequest
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
        var appUserId = User.FindFirstValue("sub");

        if (appUserId == null)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var directCustomer = Route<Guid>("directCustomerId");

        var result = await handler.HandleAsync(Guid.Parse(appUserId), directCustomer, cancellationToken);

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
