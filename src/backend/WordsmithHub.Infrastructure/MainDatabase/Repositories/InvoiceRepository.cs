using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.InvoiceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class InvoiceRepository(MainDbContext context) : Repository<Invoice>(context), IInvoiceRepository
{
    public async Task<IReadOnlyList<Invoice>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Invoices.AsNoTracking()
            .Where(s => s.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }
}
