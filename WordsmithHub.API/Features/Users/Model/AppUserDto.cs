namespace WordsmithHub.API.Features.Users.Model;

public record AppUserDto
{
    public required Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public string? PhoneNumber { get; set; }
}
