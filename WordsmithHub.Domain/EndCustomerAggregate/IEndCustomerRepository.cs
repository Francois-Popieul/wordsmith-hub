namespace WordsmithHub.Domain.EndCustomerAggregate;

public interface IEndCustomerRepository : IRepository<EndCustomer>
{
    Task<IReadOnlyList<EndCustomer>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsWithNameAsync(
        Guid freelanceId,
        string name,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EndCustomer>> SearchAsync(
        Guid freelanceId,
        string query,
        CancellationToken cancellationToken = default);
}
