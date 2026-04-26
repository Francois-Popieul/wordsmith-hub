using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class LegalStatusRepository(MainDbContext context) : Repository<LegalStatus>(context), ILegalStatusRepository
{
    public async Task<IReadOnlyList<LegalStatus>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.LegalStatuses.AsNoTracking()
            .Where(s => s.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }
}
