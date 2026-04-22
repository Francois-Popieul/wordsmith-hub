using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.OrderLineAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class OrderLineRepository(MainDbContext context) : Repository<OrderLine>(context), IOrderLineRepository
{
    public async Task<IReadOnlyList<OrderLine>> GetByWorkOrderIdAsync(Guid workOrderId,
        CancellationToken cancellationToken = default)
    {
        return await Context.OrderLines.AsNoTracking()
            .Where(l => l.WorkOrderId == workOrderId)
            .ToListAsync(cancellationToken);
    }
}
