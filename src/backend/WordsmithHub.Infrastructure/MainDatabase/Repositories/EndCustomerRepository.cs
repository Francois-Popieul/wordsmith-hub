using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.EndCustomerAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class EndCustomerRepository(MainDbContext context) : Repository<EndCustomer>(context), IEndCustomerRepository
{
    public async Task<IReadOnlyList<EndCustomer>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.EndCustomers.AsNoTracking()
            .Where(c => Context.Projects
                .Any(p => p.FreelanceId == freelanceId && p.EndCustomerId == c.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsWithNameAsync(
        Guid freelanceId,
        string name,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects
            .AnyAsync(
                p => p.FreelanceId == freelanceId
                     && p.EndCustomer != null
                     && p.EndCustomer.Name == name,
                cancellationToken);
    }

    public async Task<IReadOnlyList<EndCustomer>> SearchAsync(
        Guid freelanceId,
        string query,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects.AsNoTracking()
            .Where(p => p.FreelanceId == freelanceId
                        && p.EndCustomer != null
                        && p.EndCustomer.Name.Contains(query))
            .Select(p => p.EndCustomer!)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
