namespace WordsmithHub.Domain;

public class Country
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; } // ISO 3166-1 alpha-3: 3 letters
    public required bool IsEuropeanUnionMember { get; set; } = false;
}
