namespace WordsmithHub.Domain.BankAccountAggregate;

public interface IBankAccountRepository
{
    Task<bool> ExistsWithIbanAsync(string iban, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BankAccount>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default);

    Task<BankAccount?> GetByIbanAsync(string iban, CancellationToken cancellationToken = default);

    Task<bool> BelongsToFreelanceAsync(Guid bankAccountId, Guid freelanceId,
        CancellationToken cancellationToken = default);

    Task<BankAccount?> GetDefaultForFreelanceAsync(Guid freelanceId, CancellationToken cancellationToken = default);

    Task<bool> HasAnyAsync(Guid freelanceId, CancellationToken cancellationToken = default);

    Task<int> CountForFreelanceAsync(Guid freelanceId, CancellationToken cancellationToken = default);
}
