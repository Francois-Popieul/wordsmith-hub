using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class ServiceRepository(MainDbContext context) : IServiceRepository
{
    public async Task<IReadOnlyList<Service>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Services
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
