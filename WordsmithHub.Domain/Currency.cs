namespace WordsmithHub.Domain;

public class Currency
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; } // ISO 4217: 3 letters
    public required string Symbol { get; set; }
}
