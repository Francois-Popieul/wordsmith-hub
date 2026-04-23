using JetBrains.Annotations;

namespace WordsmithHub.Domain;

[UsedImplicitly]
public class Address
{
    public required string StreetInfo { get; set; }
    public string? AddressComplement { get; set; }
    public required string PostCode { get; set; }
    public required string City { get; set; }
    public string? State { get; set; }
    public required int CountryId { get; set; }
}
