using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.BankAccounts.Update;

public record UpdateBankAccountCommand(
    Guid AppUserId,
    Guid BankAccountId,
    string Label,
    string BankName,
    string AccountHolderName,
    string Iban,
    string Bic,
    bool IsDefault) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateBankAccountHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IBankAccountRepository repository)
    : ICommandHandler<UpdateBankAccountCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(
        UpdateBankAccountCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<BankAccount>(command.AppUserId,
                command.BankAccountId,
                cancellationToken))
        {
            return OperationResult.Forbidden<Guid>();
        }

        var bankAccount = await repository.GetByIdAsync(command.BankAccountId, cancellationToken);

        if (bankAccount == null)
        {
            return OperationResult.NotFound<Guid>();
        }

        bankAccount.Label = command.Label;
        bankAccount.BankName = command.BankName;
        bankAccount.AccountHolderName = command.AccountHolderName;
        bankAccount.Iban = command.Iban;
        bankAccount.Bic = command.Bic;
        bankAccount.IsDefault = command.IsDefault;
        bankAccount.UpdatedAt = DateTimeOffset.UtcNow;

        await repository.UpdateAsync(bankAccount, cancellationToken);

        return OperationResult.Success(bankAccount.Id);
    }
}
