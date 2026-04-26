using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class DirectCustomerRepository(MainDbContext context)
    : Repository<DirectCustomer>(context), IDirectCustomerRepository
{
    public async Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await Context.DirectCustomers.AnyAsync(c => c.Name == name,
            cancellationToken);
    }

    public async Task<IReadOnlyList<DirectCustomer>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.DirectCustomers.AsNoTracking().Where(c => c.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }

    public async Task ArchiveAsync(DirectCustomer customer, CancellationToken cancellationToken = default)
    {
        await UpdateAsync(customer, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
