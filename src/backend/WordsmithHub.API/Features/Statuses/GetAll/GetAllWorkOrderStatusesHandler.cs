using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Statuses.GetAll;

public record GetAllWorkOrderStatusesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<Status>>>;

public class GetAllWorkOrderStatusesHandler(
    IFreelanceRepository freelanceRepository,
    IStatusRepository statusRepository)
    : ICommandHandler<GetAllWorkOrderStatusesCommand, OperationResult<IReadOnlyList<Status>>>
{
    public async Task<OperationResult<IReadOnlyList<Status>>> ExecuteAsync(
        GetAllWorkOrderStatusesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<Status>>(OperationStatus.Forbidden);

        var workOrderStatuses = await statusRepository.GetAllWorkOrderStatusesAsync(cancellationToken);

        return workOrderStatuses.Count == 0
            ? new OperationResult<IReadOnlyList<Status>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<Status>>(OperationStatus.Success, workOrderStatuses);
    }
}
