namespace WordsmithHub.Domain.OrderLineAggregate;

public interface IOrderLineRepository : IRepository<OrderLine>
{
    Task<IReadOnlyList<OrderLine>> GetByWorkOrderIdAsync(
        Guid workOrderId,
        CancellationToken cancellationToken = default);
}
