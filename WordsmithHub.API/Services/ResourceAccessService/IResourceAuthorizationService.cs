using WordsmithHub.Domain;

namespace WordsmithHub.API.Services.ResourceAccessService;

public interface IResourceAuthorizationService
{
    Task<bool> CanAccessAsync<T>(
        Guid appUserId,
        Guid entityId,
        CancellationToken cancellationToken = default
    ) where T : BaseEntity, IBelongsToFreelance;
}
