namespace WordsmithHub.Domain.InvoiceAggregate;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<IReadOnlyList<Invoice>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);
}
