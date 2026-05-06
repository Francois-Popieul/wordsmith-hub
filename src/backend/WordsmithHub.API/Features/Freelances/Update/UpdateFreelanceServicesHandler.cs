using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Update;

public record UpdateFreelanceServicesCommand(
    IReadOnlyList<int> ServiceIds,
    Guid AppUserId,
    Guid FreelanceId)
    : ICommand<OperationResult<Guid>>;

public class UpdateFreelanceServicesHandler(IFreelanceRepository repository)
    : ICommandHandler<UpdateFreelanceServicesCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateFreelanceServicesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await repository.GetFreelanceWithServicesByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        await repository.UpdateServicesAsync(freelance, command.ServiceIds, cancellationToken);

        return OperationResult.Success(freelance.Id);
    }
}
