namespace WordsmithHub.Domain.WorkOrderAggregate;

public class WorkOrder : BaseEntity
{
    public required string Reference { get; set; }
    public required Guid ProjectId { get; set; }
    public required Guid FreelanceId { get; set; }
    public required Guid DirectCustomerId { get; set; }
    public Guid? InvoiceId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime DeliveryDate { get; set; }
    public string? Notes { get; set; }
    public required int StatusId { get; set; }
}
