using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Domain.RateAggregate;

public class Rate : BaseEntity, IBelongsToFreelance
{
    public required decimal UnitPrice { get; set; }
    public required string Unit { get; set; }
    public required int SourceLanguageId { get; set; }
    public TranslationLanguage? SourceLanguage { get; set; }
    public required int TargetLanguageId { get; set; }
    public TranslationLanguage? TargetLanguage { get; set; }
    public required int ServiceId { get; set; }
    public Service? Service { get; set; }
    public required int StatusId { get; set; }
    public Status? Status { get; set; }
    public required Guid DirectCustomerId { get; set; }
    public DirectCustomer? DirectCustomer { get; set; }
    public required Guid FreelanceId { get; set; }
    public Freelance? Freelance { get; set; }

    public void MarkAsDeleted()
    {
        StatusId = StatusIds.General.Inactive;
    }
}
