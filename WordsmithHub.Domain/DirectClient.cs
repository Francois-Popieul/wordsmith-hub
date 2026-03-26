namespace WordsmithHub.Domain;

public class DirectClient : BaseEntity
{
    public required Guid UserId { get; set; }
    public required string CompanyName { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required int PaymentDelay { get; set; }
}
