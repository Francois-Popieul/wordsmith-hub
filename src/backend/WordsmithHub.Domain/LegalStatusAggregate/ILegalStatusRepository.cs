namespace WordsmithHub.Domain.LegalStatusAggregate;

public interface ILegalStatusRepository : IRepository<LegalStatus>
{
    Task<IReadOnlyList<LegalStatus>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);
}
