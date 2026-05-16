using Microsoft.AspNetCore.DataProtection;
using WordsmithHub.API.Features.BankAccounts.Models;
using WordsmithHub.Domain.BankAccountAggregate;

namespace WordsmithHub.API.Features.BankAccounts.Services;

public static class BankAccountExtensions
{
    public static BankAccountDto ToDto(this BankAccount bankAccount, IDataProtector protector)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);

        bankAccount.DecryptIban(protector);

        return new BankAccountDto
        {
            Id = bankAccount.Id,
            Label = bankAccount.Label,
            BankName = bankAccount.BankName,
            AccountHolderName = bankAccount.AccountHolderName,
            Iban = bankAccount.DisplayIban(),
            Bic = bankAccount.Bic,
            IsDefault = bankAccount.IsDefault,
        };
    }
}
