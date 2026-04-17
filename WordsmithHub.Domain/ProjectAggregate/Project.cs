namespace WordsmithHub.Domain.ProjectAggregate;

public class Project : BaseEntity
{
    public required string Name { get; set; }
    public required string Domain { get; set; }
    public string? Description { get; set; }
    public required Guid FreelanceId { get; set; }
    public required Guid DirectCustomerId { get; set; }
    public Guid? EndCustomerId { get; set; }
    public required int StatusId { get; set; }
}
