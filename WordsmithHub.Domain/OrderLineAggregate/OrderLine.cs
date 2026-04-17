namespace WordsmithHub.Domain.OrderLineAggregate;

public class OrderLine : BaseEntity
{
    public required decimal Quantity { get; set; }
    public required decimal AppliedUnitPrice { get; set; }
    public required string UsedUnit { get; set; }
    public required Guid WorkOrderId { get; set; }
    public required int SourceLanguageId { get; set; }
    public required int TargetLanguageId { get; set; }
    public required int ServiceId { get; set; }
    public string[]? Filenames { get; set; }
    public string? Notes { get; set; }
}
