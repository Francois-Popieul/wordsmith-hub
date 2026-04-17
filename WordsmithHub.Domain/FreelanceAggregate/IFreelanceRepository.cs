namespace WordsmithHub.Domain.FreelanceAggregate;

public interface IFreelanceRepository
{
    Task<Freelance?> GetByAppUserIdAsync(Guid appUserId, CancellationToken cancellationToken = default);

    Task<bool> ExistsForAppUserAsync(Guid appUserId, CancellationToken cancellationToken = default);

    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Freelance>> GetByStatusAsync(int statusId, CancellationToken cancellationToken = default);
}
