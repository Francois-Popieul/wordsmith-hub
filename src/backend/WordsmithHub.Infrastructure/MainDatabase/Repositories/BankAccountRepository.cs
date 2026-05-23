using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;
using WordsmithHub.Domain.BankAccountAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class BankAccountRepository(MainDbContext context, IDataProtectionProvider dataProtectionProvider)
    : Repository<BankAccount>(context), IBankAccountRepository
{
    private IDataProtector CreateProtector() =>
        dataProtectionProvider.CreateProtector("BankAccount.Iban");

    public async Task<bool> ExistsWithIbanAsync(string iban, CancellationToken cancellationToken = default)
    {
        var accounts = await Context.BankAccounts
            .Where(a => a.StatusId != StatusIds.General.Inactive)
            .ToListAsync(cancellationToken);

        var protector = CreateProtector();
        return accounts.Any(a =>
        {
            try { return protector.Unprotect(a.Iban) == iban; }
            catch { return false; }
        });
    }

    public async Task<IReadOnlyList<BankAccount>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.BankAccounts.AsNoTracking()
            .Where(a => a.FreelanceId == freelanceId && a.StatusId != StatusIds.General.Inactive)
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

    public async Task ArchiveAsync(BankAccount bankAccount, CancellationToken cancellationToken = default)
    {
        Context.Entry(bankAccount).Property(x => x.StatusId).IsModified = true;
        Context.Entry(bankAccount).Property(x => x.UpdatedAt).IsModified = true;
        await Context.SaveChangesAsync(cancellationToken);
    }
}
