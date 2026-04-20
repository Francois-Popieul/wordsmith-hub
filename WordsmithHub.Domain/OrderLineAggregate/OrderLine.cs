using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.Domain.OrderLineAggregate;

public class OrderLine : BaseEntity
{
    public required decimal Quantity { get; set; }
    public required decimal AppliedUnitPrice { get; set; }
    public required string UsedUnit { get; set; }
    public required Guid WorkOrderId { get; set; }
    public WorkOrder? WorkOrder { get; set; }
    public required int SourceLanguageId { get; set; }
    public TranslationLanguage? SourceLanguage { get; set; }
    public required int TargetLanguageId { get; set; }
    public TranslationLanguage? TargetLanguage { get; set; }
    public required int ServiceId { get; set; }
    public Service? Service { get; set; }
    public string? Notes { get; set; }
}
