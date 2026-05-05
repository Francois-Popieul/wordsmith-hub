using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Services.FreelanceAccessService;

public interface IFreelanceAccessService
{
    Task<Freelance?> GetFreelanceForUserAsync(Guid appUserId, CancellationToken cancellationToken);

    Task<Freelance?> GetFreelanceProfileAsync(Guid appUserId, CancellationToken cancellationToken);
}