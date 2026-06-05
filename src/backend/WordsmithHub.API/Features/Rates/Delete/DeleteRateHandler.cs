using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.API.Features.Rates.Delete;

public record DeleteRateCommand(Guid AppUserId, Guid RateId) : ICommand<OperationResult<NoContent>>;

[UsedImplicitly]
public class DeleteRateHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IRateRepository rateRepository)
    : ICommandHandler<DeleteRateCommand, OperationResult<NoContent>>
{
    public async Task<OperationResult<NoContent>> ExecuteAsync(DeleteRateCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<Rate>(command.AppUserId,
                command.RateId, cancellationToken))
            return OperationResult.Forbidden<NoContent>();

        var rate = await rateRepository.GetByIdAsync(command.RateId, cancellationToken);

        if (rate == null)
            return OperationResult.NotFound<NoContent>();

        rate.MarkAsDeleted();

        await rateRepository.ArchiveAsync(rate, cancellationToken);

        return OperationResult.Success(new NoContent());
    }
}
