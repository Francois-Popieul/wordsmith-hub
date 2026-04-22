namespace WordsmithHub.Domain.RateAggregate;

public interface IRateFactory
{
    Rate CreateRate(
        decimal unitPrice,
        string unit,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        Guid directCustomerId,
        Guid freelanceId);
}

public class RateFactory : IRateFactory
{
    public Rate CreateRate(
        decimal unitPrice,
        string unit,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        Guid directCustomerId,
        Guid freelanceId)

    {
        var rate = new Rate
        {
            Id = Guid.NewGuid(),
            UnitPrice = unitPrice,
            Unit = unit,
            SourceLanguageId = sourceLanguageId,
            TargetLanguageId = targetLanguageId,
            ServiceId = serviceId,
            StatusId = StatusIds.General.Active,
            DirectCustomerId = directCustomerId,
            FreelanceId = freelanceId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return rate;
    }
}
