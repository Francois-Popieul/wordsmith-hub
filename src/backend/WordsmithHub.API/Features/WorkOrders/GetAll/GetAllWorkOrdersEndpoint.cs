using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.WorkOrders.Models;

namespace WordsmithHub.API.Features.WorkOrders.GetAll;

public class GetAllWorkOrdersEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<WorkOrderDto>>
{
    public override void Configure()
    {
        Get("/work-orders");
        Roles("user");
        Description(x => x.WithTags("work-orders")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllWorkOrdersCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}