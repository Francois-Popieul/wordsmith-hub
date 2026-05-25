using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class StatusRepository(MainDbContext context) : IStatusRepository
{
    public async Task<IReadOnlyList<Status>> GetAllProjectStatusesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Statuses
            .AsNoTracking()
            .Where(s => s.Category == "Project")
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Status>> GetAllInvoiceStatusesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Statuses
            .AsNoTracking()
            .Where(s => s.Category == "Invoice")
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Status>> GetAllWorkOrderStatusesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Statuses
            .AsNoTracking()
            .Where(s => s.Category == "WorkOrder")
            .ToListAsync(cancellationToken);
    }
}
