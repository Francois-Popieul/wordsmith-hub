using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class LegalStatusRepository : Repository<LegalStatus>, ILegalStatusRepository
{
    public LegalStatusRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<LegalStatus>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.LegalStatuses.AsNoTracking()
            .Where(s => s.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }
}
