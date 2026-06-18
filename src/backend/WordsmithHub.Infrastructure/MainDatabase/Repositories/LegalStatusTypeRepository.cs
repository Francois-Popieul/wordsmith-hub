using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class LegalStatusTypeRepository(MainDbContext context) : ILegalStatusTypeRepository
{
    public async Task<IReadOnlyList<LegalStatusType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.LegalStatusTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
