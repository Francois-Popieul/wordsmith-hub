namespace WordsmithHub.Domain.RateAggregate;

public class Rate : BaseEntity
{
    public required decimal UnitPrice { get; set; }
    public required string Unit { get; set; }
    public required int SourceLanguageId { get; set; }
    public required int TargetLanguageId { get; set; }
    public required int ServiceId { get; set; }
    public required int StatusId { get; set; }
    public required Guid DirectCustomerId { get; set; }
    public required Guid FreelanceId { get; set; }
}
