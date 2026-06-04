using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class PricingUnitRepository(MainDbContext context) : IPricingUnitRepository
{
    public async Task<IReadOnlyList<PricingUnit>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.PricingUnits
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}