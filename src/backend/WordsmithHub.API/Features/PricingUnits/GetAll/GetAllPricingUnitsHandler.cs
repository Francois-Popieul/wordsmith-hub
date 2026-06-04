using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.PricingUnits.GetAll;

public record GetAllPricingUnitsCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<PricingUnit>>>;

[UsedImplicitly]
public class GetAllCountriesHandler(
    IFreelanceRepository freelanceRepository,
    IPricingUnitRepository repository)
    : ICommandHandler<GetAllPricingUnitsCommand, OperationResult<IReadOnlyList<PricingUnit>>>
{
    public async Task<OperationResult<IReadOnlyList<PricingUnit>>> ExecuteAsync(
        GetAllPricingUnitsCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<PricingUnit>>(OperationStatus.Forbidden);
        }

        var countries = await repository.GetAllAsync(cancellationToken);

        return countries.Count == 0
            ? new OperationResult<IReadOnlyList<PricingUnit>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<PricingUnit>>(OperationStatus.Success, countries);
    }
}