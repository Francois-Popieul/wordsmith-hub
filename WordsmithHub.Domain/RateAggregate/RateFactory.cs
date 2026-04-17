namespace WordsmithHub.Domain.RateAggregate;

public interface IRateFactory
{
    Rate CreateRate(
        decimal unitPrice,
        string unit,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        int directCustomerId,
        int freelanceId);
}

public class RateFactory : IRateFactory
{
    public Rate CreateRate(
        decimal unitPrice,
        string unit,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        int directCustomerId,
        int freelanceId)

    {
        var rate = new Rate
        {
            Id = Guid.NewGuid(),
            UnitPrice = unitPrice,
            Unit = unit,
            SourceLanguageId = sourceLanguageId,
            TargetLanguageId = targetLanguageId,
            ServiceId = serviceId,
            StatusId = 1,
            DirectCustomerId = directCustomerId,
            FreelanceId = freelanceId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return rate;
    }
}
