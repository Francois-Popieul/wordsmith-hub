using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class DomainTypeRepository(MainDbContext context) : IDomainTypeRepository
{
    public async Task<IReadOnlyList<DomainType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.DomainTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
