namespace WordsmithHub.Domain;

public interface IStatusRepository
{
    Task<IReadOnlyList<Status>> GetAllProjectStatusesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Status>> GetAllInvoiceStatusesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Status>> GetAllWorkOrderStatusesAsync(CancellationToken cancellationToken = default);
}