using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.BankAccounts.Update;

public record UpdateDefaultBankAccountCommand(
    Guid AppUserId,
    Guid BankAccountId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateDefaultBankAccountHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IBankAccountRepository repository)
    : ICommandHandler<UpdateDefaultBankAccountCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(
        UpdateDefaultBankAccountCommand command,
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

        if (!bankAccount.IsDefault)
        {
            var defaultBankAccount = await repository.GetDefaultForFreelanceAsync(freelance.Id, cancellationToken);

            if (defaultBankAccount != null)
            {
                defaultBankAccount.IsDefault = false;
                defaultBankAccount.UpdatedAt = DateTimeOffset.UtcNow;
                await repository.UpdateIsDefaultAsync(defaultBankAccount, cancellationToken);
            }

            bankAccount.IsDefault = true;
            bankAccount.UpdatedAt = DateTimeOffset.UtcNow;
            await repository.UpdateIsDefaultAsync(bankAccount, cancellationToken);
        }

        return OperationResult.Success(bankAccount.Id);
    }
}
