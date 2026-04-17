namespace WordsmithHub.Domain.DirectCustomerAggregate;

public interface IDirectCustomerRepository
{
    Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DirectCustomer>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default);
}
