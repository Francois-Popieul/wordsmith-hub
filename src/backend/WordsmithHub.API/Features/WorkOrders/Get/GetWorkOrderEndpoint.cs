using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.WorkOrders.Models;

namespace WordsmithHub.API.Features.WorkOrders.Get;

public class GetWorkOrderEndpoint : ApiEndpointWithoutRequest<WorkOrderDto>
{
    public override void Configure()
    {
        Get("/work-order/{workOrderId:guid}");
        Roles("user");
        Description(x => x.WithTags("work-order")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var workOrderId = Route<Guid>("workOrderId");

        var command = new GetWorkOrderCommand(
            workOrderId,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
