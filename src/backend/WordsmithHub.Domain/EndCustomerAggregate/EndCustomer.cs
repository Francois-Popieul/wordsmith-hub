namespace WordsmithHub.Domain.EndCustomerAggregate;

public class EndCustomer : BaseEntity
{
    public required string Name { get; set; }
    public required int StatusId { get; set; }
    public Status? Status { get; set; }

    public void MarkAsDeleted()
    {
        StatusId = StatusIds.General.Inactive;
    }
}
