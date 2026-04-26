using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.InvoiceAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.Domain.WorkOrderAggregate;

public class WorkOrder : BaseEntity, IBelongsToFreelance
{
    public required string Reference { get; set; }
    public required Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public required Guid FreelanceId { get; set; }
    public Freelance? Freelance { get; set; }
    public required Guid DirectCustomerId { get; set; }
    public DirectCustomer? DirectCustomer { get; set; }
    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime DeliveryDate { get; set; }
    public string? Description { get; set; }
    public required int StatusId { get; set; }
    public Status? Status { get; set; }
}
