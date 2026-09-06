using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class RateRepository(MainDbContext context) : Repository<Rate>(context), IRateRepository
{
    public async Task<IReadOnlyList<Rate>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Rates.AsNoTracking()
            .Where(r => r.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Rate>> GetByDirectCustomerIdAsync(Guid directCustomerId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Rates.AsNoTracking()
            .Where(r => r.DirectCustomerId == directCustomerId && r.StatusId != StatusIds.General.Inactive)
            .Include(r => r.DirectCustomer)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Rate>> GetByDirectCustomerNameAsync(string directCustomerName, Guid excludeFreelanceId,
        CancellationToken cancellationToken = default)
    {
        var name = directCustomerName.ToLower();

        return await Context.Rates.AsNoTracking()
            .Where(r => r.FreelanceId != excludeFreelanceId
                        && r.StatusId != StatusIds.General.Inactive
                        && r.DirectCustomer!.Name.ToLower() == name)
            .ToListAsync(cancellationToken);
    }

    public async Task ArchiveAsync(Rate rate, CancellationToken cancellationToken = default)
    {
        Context.Entry(rate).Property(x => x.StatusId).IsModified = true;
        Context.Entry(rate).Property(x => x.UpdatedAt).IsModified = true;
        await Context.SaveChangesAsync(cancellationToken);
    }
}
