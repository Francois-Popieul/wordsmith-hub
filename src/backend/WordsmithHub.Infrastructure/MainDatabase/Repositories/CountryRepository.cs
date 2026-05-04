using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class CountryRepository(MainDbContext context) : ICountryRepository
{
    public async Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Countries
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
