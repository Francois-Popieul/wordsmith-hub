using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Services.FreelanceAccessService;

public class FreelanceAccessService(MainDbContext context) : IFreelanceAccessService
{
    public Task<Freelance?> GetFreelanceForUserAsync(Guid appUserId, CancellationToken cancellationToken)
    {
        return context.Freelances
            .SingleOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
    }

    public Task<Freelance?> GetFreelanceProfileAsync(Guid appUserId, CancellationToken cancellationToken)
    {
        return context.Freelances
            .Include(f => f.SourceLanguages)
            .Include(f => f.TargetLanguages)
            .Include(f => f.Services)
            .SingleOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);
    }
}
