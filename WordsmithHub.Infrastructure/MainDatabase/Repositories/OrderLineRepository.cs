using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.OrderLineAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class OrderLineRepository : Repository<OrderLine>, IOrderLineRepository
{
    public OrderLineRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<OrderLine>> GetByWorkOrderIdAsync(Guid workOrderId,
        CancellationToken cancellationToken = default)
    {
        return await Context.OrderLines.AsNoTracking()
            .Where(l => l.WorkOrderId == workOrderId)
            .ToListAsync(cancellationToken);
    }
}
