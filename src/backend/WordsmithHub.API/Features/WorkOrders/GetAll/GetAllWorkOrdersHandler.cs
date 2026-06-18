using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.WorkOrders.Models;
using WordsmithHub.API.Features.WorkOrders.Services;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.API.Features.WorkOrders.GetAll;

public record GetAllWorkOrdersCommand(Guid AppUserId) : ICommand<OperationResult<IReadOnlyList<WorkOrderDto>>>;

[UsedImplicitly]
public class GetAllProjectsHandler(
    IFreelanceRepository freelanceRepository,
    IWorkOrderRepository workOrderRepository)
    : ICommandHandler<GetAllWorkOrdersCommand, OperationResult<IReadOnlyList<WorkOrderDto>>>
{
    public async Task<OperationResult<IReadOnlyList<WorkOrderDto>>> ExecuteAsync(
        GetAllWorkOrdersCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<WorkOrderDto>>(OperationStatus.Forbidden);

        var workOrders = await workOrderRepository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (workOrders.Count == 0)
            return new OperationResult<IReadOnlyList<WorkOrderDto>>(OperationStatus.Success, []);

        var workOrderDtoList = workOrders.Select(workOrder => workOrder.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<WorkOrderDto>>(OperationStatus.Success, workOrderDtoList);
    }
}
