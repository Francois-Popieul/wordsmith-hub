namespace WordsmithHub.Domain.WorkOrderAggregate;

public interface IWorkOrderRepository : IRepository<WorkOrder>
{
    Task<IReadOnlyList<WorkOrder>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WorkOrder>> GetByDirectCustomerIdAsync(
        Guid freelanceId,
        Guid directCustomerId,
        CancellationToken cancellationToken = default);
}
