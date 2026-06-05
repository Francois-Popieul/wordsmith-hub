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
    : ICommandHandler<GetAllInvoiceStatusesCommand, OperationResult<IReadOnlyList<Status>>>
{
    public async Task<OperationResult<IReadOnlyList<Status>>> ExecuteAsync(
        GetAllInvoiceStatusesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<Status>>(OperationStatus.Forbidden);

        var invoiceStatuses = await statusRepository.GetAllInvoiceStatusesAsync(cancellationToken);

        return invoiceStatuses.Count == 0
            ? new OperationResult<IReadOnlyList<Status>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<Status>>(OperationStatus.Success, invoiceStatuses);
    }
}
