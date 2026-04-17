using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.Domain.DirectCustomerAggregate;

public class DirectCustomer : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Phone { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public string? SiretOrSiren { get; set; }
    public required int PaymentDelay { get; set; }
    public required Guid FreelanceId { get; set; }
    public required int CurrencyId { get; set; }
    public required int StatusId { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
