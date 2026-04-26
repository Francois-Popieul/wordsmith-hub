namespace WordsmithHub.Domain.FreelanceAggregate;

public class Freelance : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public Address? Address { get; set; }
    public required Guid AppUserId { get; set; }
    public required int StatusId { get; set; }
    public Status? Status { get; set; }
    public ICollection<TranslationLanguage> SourceLanguages { get; set; } = new List<TranslationLanguage>();
    public ICollection<TranslationLanguage> TargetLanguages { get; set; } = new List<TranslationLanguage>();
    public ICollection<Service> Services { get; set; } = new List<Service>();

    public void MarkAsDeleted()
    {
        StatusId = StatusIds.General.Inactive;
    }
}
