using WordsmithHub.API.Features.Common.Models;

namespace WordsmithHub.API.Features.Freelances.Models;

public record FreelanceDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Phone { get; set; }
    public required string Email { get; set; }
    public required AddressDto? Address { get; set; }
    public required int StatusId { get; set; }
}
