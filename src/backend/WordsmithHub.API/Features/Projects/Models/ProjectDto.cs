using WordsmithHub.API.Features.DirectCustomers.Models;

namespace WordsmithHub.API.Features.Projects.Models;

public record ProjectDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Domain { get; set; }
    public string? Description { get; set; }
    public EndCustomerDto? EndCustomer { get; set; }
    public required ICollection<DirectCustomerDto> DirectCustomers { get; set; }
    public int StatusId { get; set; }
}
