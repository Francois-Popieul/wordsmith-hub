using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.BankAccountAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class BankAccountRepository(MainDbContext context) : Repository<BankAccount>(context), IBankAccountRepository
{
    public async Task<bool> ExistsWithIbanAsync(string iban, CancellationToken cancellationToken = default)
    {
        return await Context.BankAccounts.AnyAsync(a => a.Iban == iban, cancellationToken);
    }

    public async Task<IReadOnlyList<BankAccount>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.BankAccounts.AsNoTracking().Where(a => a.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }

    public async Task<BankAccount?> GetByIbanAsync(string iban, CancellationToken cancellationToken = default)
    {
        return await Context.BankAccounts.FirstOrDefaultAsync(a => a.Iban == iban, cancellationToken);
    }

    public async Task<bool> BelongsToFreelanceAsync(Guid bankAccountId, Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.BankAccounts.AnyAsync(a => a.Id == bankAccountId && a.FreelanceId == freelanceId,
            cancellationToken);
    }

    public async Task<BankAccount?> GetDefaultForFreelanceAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.BankAccounts.SingleOrDefaultAsync(a => a.FreelanceId == freelanceId && a.IsDefault,
            cancellationToken);
    }

    public Task<bool> HasAnyAsync(Guid freelanceId, CancellationToken cancellationToken = default)
    {
        return Context.BankAccounts.AnyAsync(a => a.FreelanceId == freelanceId, cancellationToken);
    }

    public Task<int> CountForFreelanceAsync(Guid freelanceId, CancellationToken cancellationToken = default)
    {
        return Context.BankAccounts.CountAsync(a => a.FreelanceId == freelanceId, cancellationToken);
    }
}
