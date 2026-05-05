using WordsmithHub.API.Features.Common.Models;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Freelances.Models;

public record ProfileDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Phone { get; set; }
    public required string Email { get; set; }
    public required AddressDto? Address { get; set; }
    public required int StatusId { get; set; }
    public IReadOnlyList<TranslationLanguage> SourceLanguages { get; set; } = [];
    public IReadOnlyList<TranslationLanguage> TargetLanguages { get; set; } = [];
    public IReadOnlyList<Service> Services { get; set; } = [];
}
