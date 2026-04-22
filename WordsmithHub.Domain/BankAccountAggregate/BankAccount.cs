using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Domain.BankAccountAggregate;

public class BankAccount : BaseEntity, IBelongsToFreelance
{
    public required string Label { get; set; }
    public required string BankName { get; set; }
    public required string AccountHolderName { get; set; }
    public required string Iban { get; set; }
    public required string Bic { get; set; }
    public required bool IsDefault { get; set; }
    public required Guid FreelanceId { get; set; }
    public Freelance? Freelance { get; set; }
    public required int StatusId { get; set; }
    public Status? Status { get; set; }

    public void MarkAsDeleted()
    {
        StatusId = StatusIds.General.Inactive;
    }
}
