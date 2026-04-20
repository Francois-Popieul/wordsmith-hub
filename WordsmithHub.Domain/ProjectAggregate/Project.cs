using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.EndCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Domain.ProjectAggregate;

public class Project : BaseEntity
{
    public required string Name { get; set; }
    public required string Domain { get; set; }
    public string? Description { get; set; }
    public required Guid FreelanceId { get; set; }
    public Freelance? Freelance { get; set; }
    public Guid? EndCustomerId { get; set; }
    public EndCustomer? EndCustomer { get; set; }
    public required int StatusId { get; set; }
    public Status? Status { get; set; }
    public ICollection<DirectCustomer> DirectCustomers { get; set; } = new List<DirectCustomer>();
}
