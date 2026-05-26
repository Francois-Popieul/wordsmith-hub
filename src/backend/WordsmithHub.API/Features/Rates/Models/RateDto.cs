namespace WordsmithHub.API.Features.Rates.Models;

public record RateDto
{
    public required Guid Id { get; set; }
    public required decimal UnitPrice { get; set; }
    public required string Unit { get; set; }
    public required int SourceLanguageId { get; set; }
    public required int TargetLanguageId { get; set; }

    public required int ServiceId { get; set; }
    public required Guid DirectCustomerId { get; set; }
}
