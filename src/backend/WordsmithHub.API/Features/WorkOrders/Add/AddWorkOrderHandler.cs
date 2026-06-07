using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.ProjectAggregate;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.API.Features.WorkOrders.Add;

public record AddWorkOrderCommand(
    string Reference,
    Guid ProjectId,
    Guid FreelanceId,
    Guid DirectCustomerId,
    DateTime StartDate,
    DateTime DeliveryDate,
    string? Description,
    Guid AppUserId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class AddWorkOrderHandler(
    IFreelanceRepository freelanceRepository,
    IProjectRepository projectRepository,
    IWorkOrderRepository workOrderRepository,
    IDirectCustomerRepository directCustomerRepository,
    IWorkOrderFactory workOrderFactory) : ICommandHandler<AddWorkOrderCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(AddWorkOrderCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return OperationResult.Forbidden<Guid>();

        var directCustomer = await directCustomerRepository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        if (directCustomer == null || directCustomer.FreelanceId != freelance.Id)
            return OperationResult.Error<Guid>();

        var project = await projectRepository.GetByIdAsync(command.ProjectId, cancellationToken);

        if (project == null ||
            !project.DirectCustomers.Any(dc => dc.Id == directCustomer.Id))
            return OperationResult.Error<Guid>();

        if (project.FreelanceId != freelance.Id)
            return OperationResult.Forbidden<Guid>();

        var workOrder = workOrderFactory.CreateWorkOrder(
            command.Reference,
            command.ProjectId,
            freelance.Id,
            command.DirectCustomerId,
            command.StartDate,
            command.DeliveryDate,
            command.Description);

        await workOrderRepository.AddAsync(workOrder, cancellationToken);

        return OperationResult.Success(workOrder.Id);
    }
}
