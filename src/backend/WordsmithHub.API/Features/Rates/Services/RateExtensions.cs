using WordsmithHub.API.Features.Rates.Models;
using WordsmithHub.Domain.RateAggregate;

namespace WordsmithHub.API.Features.Rates.Services;

public static class RateExtensions
{
    public static RateDto ToDto(this Rate rate)
    {
        ArgumentNullException.ThrowIfNull(rate);

        return new RateDto
        {
            Id = rate.Id,
            UnitPrice = rate.UnitPrice,
            Unit = rate.Unit,
            SourceLanguageId = rate.SourceLanguageId,
            TargetLanguageId = rate.TargetLanguageId,
            ServiceId = rate.ServiceId,
            DirectCustomerId = rate.DirectCustomerId
        };
    }
}
