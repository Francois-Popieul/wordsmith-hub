using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class LegalStatusRepository(MainDbContext context) : Repository<LegalStatus>(context), ILegalStatusRepository
{
    public async Task<IReadOnlyList<LegalStatus>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.LegalStatuses.AsNoTracking()
            .Where(s => s.FreelanceId == freelanceId && s.StatusId != StatusIds.General.Inactive)
            .ToListAsync(cancellationToken);
    }

    public async Task ArchiveAsync(LegalStatus legalStatus, CancellationToken cancellationToken = default)
    {
        Context.Entry(legalStatus).Property(x => x.StatusId).IsModified = true;
        Context.Entry(legalStatus).Property(x => x.UpdatedAt).IsModified = true;
        await Context.SaveChangesAsync(cancellationToken);
    }
}
