using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class FreelanceRepository(MainDbContext context) : Repository<Freelance>(context), IFreelanceRepository
{
    public async Task<Freelance?> GetByAppUserIdAsync(Guid appUserId, CancellationToken cancellationToken = default)
    {
        return await Context.Freelances.FirstOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
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

    public Task ArchiveAsync(Freelance freelance, CancellationToken cancellationToken = default)
    {
        Context.Freelances.Update(freelance);
        return Context.SaveChangesAsync(cancellationToken);
    }
}
