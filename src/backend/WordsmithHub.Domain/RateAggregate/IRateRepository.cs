namespace WordsmithHub.Domain.RateAggregate;

public interface IRateRepository : IRepository<Rate>
{
    Task<IReadOnlyList<Rate>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);
}
