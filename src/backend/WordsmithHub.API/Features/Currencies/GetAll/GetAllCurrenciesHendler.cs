using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Currencies.GetAll;

public record GetAllCurrenciesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<Currency>>>;

[UsedImplicitly]
public class GetAllCurrenciesHandler(
    IFreelanceRepository freelanceRepository,
    ICurrencyRepository repository)
    : ICommandHandler<GetAllCurrenciesCommand, OperationResult<IReadOnlyList<Currency>>>
{
    public async Task<OperationResult<IReadOnlyList<Currency>>> ExecuteAsync(
        GetAllCurrenciesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<Currency>>(OperationStatus.Forbidden);
        }

        var currencies = await repository.GetAllAsync(cancellationToken);

        if (currencies.Count == 0)
        {
            return new OperationResult<IReadOnlyList<Currency>>(OperationStatus.NotFound);
        }

        return new OperationResult<IReadOnlyList<Currency>>(OperationStatus.Success, currencies);
    }
}
