using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Rates.Models;
using WordsmithHub.API.Features.Rates.Services;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.API.Features.Rates.GetAll;

public record GetAllRatesByCustomerIdCommand(Guid AppUserId, Guid DirectCustomerId)
    : ICommand<OperationResult<IReadOnlyList<RateDto>>>;

[UsedImplicitly]
public class GetAllRatesByCustomerIdHandler(
    IResourceAuthorizationService resourceAuthorizationService,
    IFreelanceRepository freelanceRepository,
    IRateRepository rateRepository)
    : ICommandHandler<GetAllRatesByCustomerIdCommand, OperationResult<IReadOnlyList<RateDto>>>
{
    public async Task<OperationResult<IReadOnlyList<RateDto>>> ExecuteAsync(
        GetAllRatesByCustomerIdCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<DirectCustomer>(command.AppUserId,
                command.DirectCustomerId, cancellationToken))
            return new OperationResult<IReadOnlyList<RateDto>>(OperationStatus.Forbidden);

        var rates
            = await rateRepository.GetByDirectCustomerIdAsync(command.DirectCustomerId, cancellationToken);

        if (rates.Count == 0)
            return new OperationResult<IReadOnlyList<RateDto>>(OperationStatus.Success, []);

        var directCustomerName = rates[0].DirectCustomer!.Name;
        var freelanceId = rates[0].FreelanceId;

        var competitorRates
            = await rateRepository.GetByDirectCustomerNameAsync(directCustomerName, freelanceId, cancellationToken);

        var averagesByRateKey = competitorRates
            .GroupBy(r => (r.ServiceId, r.SourceLanguageId, r.TargetLanguageId, r.Unit))
            .ToDictionary(g => g.Key, g => g.Average(r => r.UnitPrice));

        var highestByRateKey = competitorRates
            .GroupBy(r => (r.ServiceId, r.SourceLanguageId, r.TargetLanguageId, r.Unit))
            .ToDictionary(g => g.Key, g => g.Max(r => r.UnitPrice));

        var lowestByRateKey = competitorRates
            .GroupBy(r => (r.ServiceId, r.SourceLanguageId, r.TargetLanguageId, r.Unit))
            .ToDictionary(g => g.Key, g => g.Min(r => r.UnitPrice));

        var rateDtoList = rates
            .Select(rate =>
            {
                averagesByRateKey.TryGetValue((rate.ServiceId, rate.SourceLanguageId, rate.TargetLanguageId, rate.Unit),
                    out var averageRate);
                highestByRateKey.TryGetValue((rate.ServiceId, rate.SourceLanguageId, rate.TargetLanguageId, rate.Unit),
                    out var highestRate);
                lowestByRateKey.TryGetValue((rate.ServiceId, rate.SourceLanguageId, rate.TargetLanguageId, rate.Unit),
                    out var lowestRate);
                return rate.ToDto(averageRate, highestRate, lowestRate);
            })
            .ToList();

        return new OperationResult<IReadOnlyList<RateDto>>(OperationStatus.Success, rateDtoList);
    }
}
