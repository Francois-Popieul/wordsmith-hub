using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.API.Features.Rates.Add;

public record AddRateCommand(
    decimal UnitPrice,
    string Unit,
    int SourceLanguageId,
    int TargetLanguageId,
    int ServiceId,
    Guid DirectCustomerId,
    Guid AppUserId) : ICommand<OperationResult<Guid>>;

public class AddRateHandler(
    IFreelanceRepository freelanceRepository,
    IRateRepository rateRepository,
    IDirectCustomerRepository directCustomerRepository,
    IRateFactory rateFactory) : ICommandHandler<AddRateCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(AddRateCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return OperationResult.Forbidden<Guid>();

        var directCustomer = await directCustomerRepository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        if (directCustomer == null)
            return OperationResult.NotFound<Guid>();

        if (directCustomer.FreelanceId != freelance.Id)
            return OperationResult.Forbidden<Guid>();

        var rate = rateFactory.CreateRate(
            command.UnitPrice,
            command.Unit,
            command.SourceLanguageId,
            command.TargetLanguageId,
            command.ServiceId,
            command.DirectCustomerId,
            freelance.Id);

        await rateRepository.AddAsync(rate, cancellationToken);

        return OperationResult.Success(rate.Id);
    }
}
