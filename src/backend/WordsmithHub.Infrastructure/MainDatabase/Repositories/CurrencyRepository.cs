using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class CurrencyRepository(MainDbContext context) : ICurrencyRepository
{
    public async Task<IReadOnlyList<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Currencies
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
