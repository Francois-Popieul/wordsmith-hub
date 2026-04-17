using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class FreelanceRepository : Repository<Freelance>, IFreelanceRepository
{
    public FreelanceRepository(MainDbContext context) : base(context)
    {
    }

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
}
