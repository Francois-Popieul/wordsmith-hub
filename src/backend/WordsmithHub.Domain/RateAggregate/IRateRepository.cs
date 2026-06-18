namespace WordsmithHub.Domain.RateAggregate;

public interface IRateRepository : IRepository<Rate>
{
    Task<IReadOnlyList<Rate>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Rate>> GetByDirectCustomerIdAsync(Guid directCustomerId,
        CancellationToken cancellationToken = default);

    Task ArchiveAsync(Rate rate, CancellationToken cancellationToken = default);
}
