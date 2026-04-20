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
}
