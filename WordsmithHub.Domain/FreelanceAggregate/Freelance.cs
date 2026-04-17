namespace WordsmithHub.Domain.FreelanceAggregate;

public class Freelance : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required Address Address { get; set; }
    public required Guid AppUserId { get; set; }
    public required int StatusId { get; set; }
}
