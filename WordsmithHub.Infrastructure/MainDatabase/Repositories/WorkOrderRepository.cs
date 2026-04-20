using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
{
    public WorkOrderRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<WorkOrder>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.WorkOrders.AsNoTracking()
            .Where(o => o.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WorkOrder>> GetByDirectCustomerIdAsync(
        Guid freelanceId,
        Guid directCustomerId,
        CancellationToken cancellationToken = default)
    {
        return await Context.WorkOrders.AsNoTracking()
            .Where(o => o.FreelanceId == freelanceId && o.DirectCustomerId == directCustomerId)
            .ToListAsync(cancellationToken);
    }
}
