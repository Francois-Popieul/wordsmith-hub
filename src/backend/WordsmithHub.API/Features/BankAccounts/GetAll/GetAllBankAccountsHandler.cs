using FastEndpoints;
using JetBrains.Annotations;
using Microsoft.AspNetCore.DataProtection;
using WordsmithHub.API.Features.BankAccounts.Models;
using WordsmithHub.API.Features.BankAccounts.Services;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.BankAccounts.GetAll;

public record GetAllBankAccountsCommand(Guid AppUserId) : ICommand<OperationResult<IReadOnlyList<BankAccountDto>>>;

[UsedImplicitly]
public class GetAllBankAccountsHandler(
    IFreelanceRepository freelanceRepository,
    IBankAccountRepository repository,
    IDataProtectionProvider dataProtectionProvider)
    : ICommandHandler<GetAllBankAccountsCommand, OperationResult<IReadOnlyList<BankAccountDto>>>
{
    private IDataProtector CreateProtector() =>
        dataProtectionProvider.CreateProtector("BankAccount.Iban");

    public async Task<OperationResult<IReadOnlyList<BankAccountDto>>> ExecuteAsync(
        GetAllBankAccountsCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<BankAccountDto>>(OperationStatus.Forbidden);
        }

        var bankAccounts
            = await repository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (bankAccounts.Count == 0)
        {
            return new OperationResult<IReadOnlyList<BankAccountDto>>(OperationStatus.Success, new List<BankAccountDto>());
        }

        var bankAccountDtoList = bankAccounts.Select(bankAccount => bankAccount.ToDto(CreateProtector())).ToList();

        return new OperationResult<IReadOnlyList<BankAccountDto>>(OperationStatus.Success, bankAccountDtoList);
    }
}
