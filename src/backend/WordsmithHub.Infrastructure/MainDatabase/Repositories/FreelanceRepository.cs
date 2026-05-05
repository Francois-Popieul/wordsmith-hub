using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class FreelanceRepository(MainDbContext context) : Repository<Freelance>(context), IFreelanceRepository
{
    public async Task<Freelance?> GetByAppUserIdAsync(Guid appUserId, CancellationToken cancellationToken = default)
    {
        return await Context.Freelances.FirstOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
    }

    public async Task<Freelance?> GetProfileByAppUserIdAsync(Guid appUserId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Freelances
            .Include(f => f.SourceLanguages)
            .Include(f => f.TargetLanguages)
            .Include(f => f.Services)
            .FirstOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
    }

    public async Task<Freelance?> GetFreelanceWithServicesByAppUserIdAsync(Guid appUserId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Freelances
            .Include(f => f.Services)
            .FirstOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
    }

    public async Task<Freelance?> GetFreelanceWithLanguagesByAppUserIdAsync(Guid appUserId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Freelances
            .Include(f => f.SourceLanguages)
            .Include(f => f.TargetLanguages)
            .FirstOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
    }

    public async Task<bool> ExistsForAppUserAsync(Guid appUserId, CancellationToken cancellationToken = default)
    {
        return await Context.Freelances.AnyAsync(f => f.AppUserId == appUserId, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Freelances.AnyAsync(f => f.Email == email, cancellationToken);
    }

    public async Task<IReadOnlyList<Freelance>> GetByStatusAsync(int statusId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Freelances.Where(f => f.StatusId == statusId).ToListAsync(cancellationToken);
    }

    public async Task UpdateServicesAsync(Freelance freelance, IReadOnlyList<int> serviceIds,
        CancellationToken cancellationToken = default)
    {
        var services = await Context.Services
            .Where(s => serviceIds.Contains(s.Id))
            .ToListAsync(cancellationToken);

        freelance.Services.Clear();
        foreach (var service in services)
            freelance.Services.Add(service);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateLanguagesAsync(Freelance freelance, IReadOnlyList<int> sourceLanguageIds,
        IReadOnlyList<int> targetLanguageIds, CancellationToken cancellationToken = default)
    {
        var sourceLanguages = await Context.TranslationLanguages
            .Where(l => sourceLanguageIds.Contains(l.Id))
            .ToListAsync(cancellationToken);

        var targetLanguages = await Context.TranslationLanguages
            .Where(l => targetLanguageIds.Contains(l.Id))
            .ToListAsync(cancellationToken);

        freelance.SourceLanguages.Clear();
        foreach (var language in sourceLanguages)
            freelance.SourceLanguages.Add(language);

        freelance.TargetLanguages.Clear();
        foreach (var language in targetLanguages)
            freelance.TargetLanguages.Add(language);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public Task ArchiveAsync(Freelance freelance, CancellationToken cancellationToken = default)
    {
        Context.Freelances.Update(freelance);
        return Context.SaveChangesAsync(cancellationToken);
    }
}
