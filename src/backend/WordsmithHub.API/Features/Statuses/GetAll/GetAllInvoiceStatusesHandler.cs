using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Statuses.GetAll;

public record GetAllInvoiceStatusesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<Status>>>;

public class GetAllInvoiceStatusesHandler(
    IFreelanceRepository freelanceRepository,
    IStatusRepository statusRepository)
    : ICommandHandler<GetAllProjectStatusesCommand, OperationResult<IReadOnlyList<Status>>>
{
    public async Task<OperationResult<IReadOnlyList<Status>>> ExecuteAsync(
        GetAllProjectStatusesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<Status>>(OperationStatus.Forbidden);
        }

        var projectStatuses = await statusRepository.GetAllInvoiceStatusesAsync(cancellationToken);

        return projectStatuses.Count == 0
            ? new OperationResult<IReadOnlyList<Status>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<Status>>(OperationStatus.Success, projectStatuses);
    }
}
