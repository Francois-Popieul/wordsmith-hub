using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Services;
using WordsmithHub.API.Features.WorkOrders.Models;
using WordsmithHub.API.Features.WorkOrders.Services;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.API.Features.WorkOrders.Get;

public record GetWorkOrderCommand(Guid WorkOrderId, Guid AppUserId) : ICommand<OperationResult<WorkOrderDto>>;

public class GetWorkOrderHandler(
    IFreelanceRepository freelanceRepository,
    IWorkOrderRepository workOrderRepository,
    IResourceAuthorizationService resourceAuthorizationService)
    : ICommandHandler<GetWorkOrderCommand, OperationResult<WorkOrderDto>>
{
    public async Task<OperationResult<WorkOrderDto>> ExecuteAsync(GetWorkOrderCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetProfileByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<WorkOrder>(command.AppUserId, command.WorkOrderId,
                cancellationToken))
            return OperationResult.Forbidden<WorkOrderDto>();

        var workOrder = await workOrderRepository.GetByIdAsync(command.WorkOrderId, cancellationToken);

        if (workOrder == null)
            return OperationResult.NotFound<WorkOrderDto>();

        var workOrderDto = workOrder.ToDto();

        return new OperationResult<WorkOrderDto>(OperationStatus.Success, workOrderDto);
    }
}
