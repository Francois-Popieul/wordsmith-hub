namespace WordsmithHub.Domain.OrderLineAggregate;

public interface IOrderLineFactory
{
    OrderLine CreateOrderLine(
        decimal quantity,
        decimal appliedUnitPrice,
        string usedUnit,
        Guid workOrderId,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        string? notes);
}

public class OrderLineFactory : IOrderLineFactory
{
    public OrderLine CreateOrderLine(
        decimal quantity,
        decimal appliedUnitPrice,
        string usedUnit,
        Guid workOrderId,
        int sourceLanguageId,
        int targetLanguageId,
        int serviceId,
        string? notes)

    {
        var orderLine = new OrderLine
        {
            Id = Guid.NewGuid(),
            Quantity = quantity,
            AppliedUnitPrice = appliedUnitPrice,
            UsedUnit = usedUnit,
            WorkOrderId = workOrderId,
            SourceLanguageId = sourceLanguageId,
            TargetLanguageId = targetLanguageId,
            ServiceId = serviceId,
            Notes = notes ?? string.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return orderLine;
    }
}
