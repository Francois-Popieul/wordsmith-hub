using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Get;

public class GetDirectCustomerEndpoint(GetDirectCustomerHandler handler)
    : EndpointWithoutRequest<DirectCustomer>
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
        var appUserId = User.FindFirstValue("sub");

        if (appUserId == null)
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var directCustomerId = Route<Guid>("directCustomerId");

        var result = await handler.HandleAsync(Guid.Parse(appUserId), directCustomerId, cancellationToken);

        switch (result.Type)
        {
            case GetResultType.Forbidden:
                await Send.ForbiddenAsync(cancellationToken);
                return;

            case GetResultType.NotFound:
                await Send.NotFoundAsync(cancellationToken);
                return;

            case GetResultType.Success:
                await Send.OkAsync(result.Entity!, cancellationToken);
                return;
        }
    }
}
