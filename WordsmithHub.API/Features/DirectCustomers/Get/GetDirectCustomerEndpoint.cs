using System.Security.Claims;
using FastEndpoints;

namespace WordsmithHub.API.Features.DirectCustomers.Get;

public class GetDirectCustomerEndpoint(GetDirectCustomerHandler handler)
    : EndpointWithoutRequest<Domain.DirectCustomerAggregate.DirectCustomer>
{
    public override void Configure()
    {
        Get("/directcustomer/{directCustomerId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("directcustomer")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var tokenUserId = User.FindFirstValue("sub");

        if (tokenUserId == null)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var command = new GetDirectCustomerCommand(DirectCustomerId: Route<Guid>("directClientId"));

        var result = await handler.HandleAsync(command, cancellationToken);

        if (result == null)
        {
            await Send.NotFoundAsync(cancellationToken);
            return;
        }

        if (result.FreelanceId != Guid.Parse(tokenUserId))
        {
            await Send.ForbiddenAsync(cancellationToken);
            return;
        }

        await Send.OkAsync(result, cancellationToken);
    }
}