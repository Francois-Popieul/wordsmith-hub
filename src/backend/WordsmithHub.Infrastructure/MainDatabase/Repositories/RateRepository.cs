using Microsoft.EntityFrameworkCore;
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
}
