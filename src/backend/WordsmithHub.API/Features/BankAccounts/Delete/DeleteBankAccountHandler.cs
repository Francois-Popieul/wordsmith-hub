using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.BankAccounts.Delete;

public record DeleteBankAccountCommand(Guid AppUserId, Guid BankAccountId) : ICommand<OperationResult<NoContent>>;

[UsedImplicitly]
public class DeleteBankAccountHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IBankAccountRepository repository)
    : ICommandHandler<DeleteBankAccountCommand, OperationResult<NoContent>>
{
    public async Task<OperationResult<NoContent>> ExecuteAsync(DeleteBankAccountCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<BankAccount>(command.AppUserId,
                command.BankAccountId, cancellationToken))
            return OperationResult.Forbidden<NoContent>();

        var bankAccount = await repository.GetByIdAsync(command.BankAccountId, cancellationToken);

        if (bankAccount == null)
            return OperationResult.NotFound<NoContent>();

        bankAccount.MarkAsDeleted();

        await repository.ArchiveAsync(bankAccount, cancellationToken);

        return OperationResult.Success(new NoContent());
    }
}
