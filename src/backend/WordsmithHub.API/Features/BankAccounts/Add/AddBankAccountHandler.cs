using FastEndpoints;
using JetBrains.Annotations;
using Microsoft.AspNetCore.DataProtection;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.BankAccounts.Add;

public record AddBankAccountCommand(
    string Label,
    string BankName,
    string AccountHolderName,
    string Iban,
    string Bic,
    Guid AppUserId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class AddBankAccountHandler(
    IFreelanceRepository freelanceRepository,
    IBankAccountRepository repository,
    IBankAccountFactory factory,
    IDataProtectionProvider dataProtectionProvider) : ICommandHandler<AddBankAccountCommand, OperationResult<Guid>>
{
    private IDataProtector CreateProtector() =>
        dataProtectionProvider.CreateProtector("BankAccount.Iban");

    public async Task<OperationResult<Guid>> ExecuteAsync(AddBankAccountCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return OperationResult.Forbidden<Guid>();
        }

        var defaultBankAccount = await repository.GetDefaultForFreelanceAsync(freelance.Id, cancellationToken);

        var bankAccount = factory.CreateBankAccount(
            freelance.Id,
            command.Label,
            command.BankName,
            command.AccountHolderName,
            command.Iban,
            command.Bic,
            isDefault: defaultBankAccount == null);

        bankAccount.EncryptIban(CreateProtector());

        await repository.AddAsync(bankAccount, cancellationToken);

        return OperationResult.Success(bankAccount.Id);
    }
}
