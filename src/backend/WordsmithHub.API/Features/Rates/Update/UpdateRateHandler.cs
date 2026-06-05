using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.API.Features.Rates.Update;

public record UpdateRateCommand(
    Guid RateId,
    decimal UnitPrice,
    string Unit,
    int SourceLanguageId,
    int TargetLanguageId,
    int ServiceId,
    Guid DirectCustomerId,
    Guid AppUserId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateRateHandler(
    IFreelanceRepository freelanceRepository,
    IRateRepository rateRepository,
    IDirectCustomerRepository directCustomerRepository,
    IResourceAuthorizationService resourceAuthorizationService)
    : ICommandHandler<UpdateRateCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateRateCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || !await resourceAuthorizationService.CanAccessAsync<Rate>(command.AppUserId,
                command.RateId, cancellationToken))
            return OperationResult.Forbidden<Guid>();

        var directCustomer = await directCustomerRepository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        if (directCustomer == null)
            return OperationResult.NotFound<Guid>();

        if (directCustomer.FreelanceId != freelance.Id)
            return OperationResult.Forbidden<Guid>();

        var rate = await rateRepository.GetByIdAsync(command.RateId, cancellationToken);

        if (rate == null)
            return OperationResult.NotFound<Guid>();

        rate.UnitPrice = command.UnitPrice;
        rate.Unit = command.Unit;
        rate.SourceLanguageId = command.SourceLanguageId;
        rate.TargetLanguageId = command.TargetLanguageId;
        rate.ServiceId = command.ServiceId;
        rate.DirectCustomerId = command.DirectCustomerId;
        rate.UpdatedAt = DateTime.UtcNow;

        await rateRepository.UpdateAsync(rate, cancellationToken);

        return OperationResult.Success(rate.Id);
    }
}
