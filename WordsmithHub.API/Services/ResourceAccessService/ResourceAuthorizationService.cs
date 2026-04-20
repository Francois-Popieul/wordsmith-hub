using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Services.ResourceAccessService;

public class ResourceAuthorizationService(MainDbContext context) : IResourceAuthorizationService
{
    public async Task<bool> CanAccessAsync<T>(
        Guid appUserId,
        Guid entityId,
        CancellationToken cancellationToken = default)
        where T : BaseEntity, IBelongsToFreelance
    {
        var freelance = await context.Freelances
            .SingleOrDefaultAsync(f => f.AppUserId == appUserId, cancellationToken);

        if (freelance == null)
            return false;

        var entity = await context.Set<T>()
            .SingleOrDefaultAsync(e => e.Id == entityId,
                cancellationToken);

        if (entity == null)
            return false;

        return entity.FreelanceId == freelance.Id;
    }
}
