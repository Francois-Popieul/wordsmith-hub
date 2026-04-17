namespace WordsmithHub.Domain.OrderLineAggregate;

public interface IOrderLineFactory
{
    OrderLine CreateOrderLine(
        decimal quantity,
        decimal appliedUnitPrice,
        Guid workOrderId,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        int rateId,
        string[]? filenames,
        string? notes);
}

public class OrderLineFactory : IOrderLineFactory
{
    public OrderLine CreateOrderLine(
        decimal quantity,
        decimal appliedUnitPrice,
        Guid workOrderId,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        int rateId,
        string[]? filenames,
        string? notes)

    {
        var orderLine = new OrderLine
        {
            Id = Guid.NewGuid(),
            Quantity = quantity,
            AppliedUnitPrice = appliedUnitPrice,
            WorkOrderId = workOrderId,
            SourceLanguageId = sourceLanguageId,
            TargetLanguageId = targetLanguageId,
            ServiceId = serviceId,
            RateId = rateId,
            Filenames = filenames ?? [],
            Notes = notes ?? string.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return orderLine;
    }
}
