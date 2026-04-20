using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class RateRepository : Repository<Rate>, IRateRepository
{
    public RateRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Rate>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Rates.AsNoTracking()
            .Where(r => r.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }
}
