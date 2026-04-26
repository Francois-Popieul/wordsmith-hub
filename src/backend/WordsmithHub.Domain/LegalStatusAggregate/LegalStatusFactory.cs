namespace WordsmithHub.Domain.LegalStatusAggregate;

public interface ILegalStatusFactory
{
    LegalStatus CreateLegalStatus(
        string name,
        string? siret,
        string? vatNumber,
        bool vatExemption,
        decimal? vatRate,
        bool taxDeductionExemption,
        DateTime validFrom,
        DateTime? validTo,
        Guid freelanceId);
}

public class LegalStatusFactory : ILegalStatusFactory
{
    public LegalStatus CreateLegalStatus(
        string name,
        string? siret,
        string? vatNumber,
        bool vatExemption,
        decimal? vatRate,
        bool taxDeductionExemption,
        DateTime validFrom,
        DateTime? validTo,
        Guid freelanceId)
    {
        var legalStatus = new LegalStatus
        {
            Name = name,
            Siret = siret ?? string.Empty,
            VatNumber = vatNumber ?? string.Empty,
            VatExemption = vatExemption,
            VatRate = vatRate,
            TaxDeductionExemption = taxDeductionExemption,
            ValidFrom = validFrom,
            ValidTo = validTo ?? null,
            FreelanceId = freelanceId,
            StatusId = StatusIds.General.Active,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return legalStatus;
    }
}
