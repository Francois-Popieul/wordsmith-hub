namespace WordsmithHub.API.Features.Projects.Models;

public record EndCustomerDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required int StatusId { get; set; }
}
