using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.LegalStatuses.Models;

public record LegalStatusDto
{
    public required Guid Id { get; set; }
    public LegalStatusType? LegalStatusType { get; set; }
    public string? Siret { get; set; }
    public string? VatNumber { get; set; }
    public required bool VatExemption { get; set; }
    public decimal? VatRate { get; set; }
    public required bool TaxDeductionExemption { get; set; }
    public required DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }
}
