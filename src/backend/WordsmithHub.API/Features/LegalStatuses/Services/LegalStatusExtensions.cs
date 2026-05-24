using WordsmithHub.API.Features.LegalStatuses.Models;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.API.Features.LegalStatuses.Services;

public static class LegalStatusExtensions
{
    public static LegalStatusDto ToDto(this LegalStatus legalStatus)
    {
        ArgumentNullException.ThrowIfNull(legalStatus);

        return new LegalStatusDto
        {
            Id = legalStatus.Id,
            Name = legalStatus.Name,
            Siret = legalStatus.Siret ?? string.Empty,
            VatNumber = legalStatus.VatNumber ?? string.Empty,
            VatExemption = legalStatus.VatExemption,
            VatRate = legalStatus.VatRate ?? 0,
            TaxDeductionExemption = legalStatus.TaxDeductionExemption,
            ValidFrom = legalStatus.ValidFrom,
            ValidTo = legalStatus.ValidTo
        };
    }
}
