namespace WordsmithHub.Domain.LegalStatusAggregate;

public class LegalStatus : BaseEntity
{
    public required string Name { get; set; }
    public string? Siret { get; set; }
    public string? VatNumber { get; set; }
    public bool VatExemption { get; set; }
    public decimal? VatRate { get; set; }
    public bool TaxDeductionExemption { get; set; }
    public required DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public required Guid FreelanceId { get; set; }
    public required int StatusId { get; set; }
}
