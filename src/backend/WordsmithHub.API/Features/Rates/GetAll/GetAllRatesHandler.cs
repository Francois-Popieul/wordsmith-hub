using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Rates.Models;
using WordsmithHub.API.Features.Rates.Services;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.API.Features.Rates.GetAll;

public record GetAllRatesCommand(Guid AppUserId) : ICommand<OperationResult<IReadOnlyList<RateDto>>>;

[UsedImplicitly]
public class GetAllRatesHandler(
    IFreelanceRepository freelanceRepository,
    IRateRepository repository)
    : ICommandHandler<GetAllRatesCommand, OperationResult<IReadOnlyList<RateDto>>>
{
    public async Task<OperationResult<IReadOnlyList<RateDto>>> ExecuteAsync(
        GetAllRatesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<RateDto>>(OperationStatus.Forbidden);

        var rates
            = await repository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (rates.Count == 0)
            return new OperationResult<IReadOnlyList<RateDto>>(OperationStatus.Success, []);

        var rateDtoList = rates.Select(rate => rate.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<RateDto>>(OperationStatus.Success, rateDtoList);
    }
}
