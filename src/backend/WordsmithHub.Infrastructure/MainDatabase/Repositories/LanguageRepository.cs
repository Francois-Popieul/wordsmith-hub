using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class LanguageRepository(MainDbContext context) : ILanguageRepository
{
    public async Task<IReadOnlyList<TranslationLanguage>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.TranslationLanguages
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
