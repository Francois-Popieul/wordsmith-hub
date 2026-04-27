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

        var entityFreelanceId = await context.Set<T>()
            .Where(e => e.Id == entityId)
            .Select(e => e.FreelanceId)
            .SingleOrDefaultAsync(cancellationToken);


        if (entityFreelanceId == Guid.Empty)
            return false;

        return entityFreelanceId == freelance.Id;
    }
}
