namespace WordsmithHub.API.Features.Common.Models;

public record AddressDto
{
    public required string StreetInfo { get; set; }
    public string? AddressComplement { get; set; }
    public required string PostCode { get; set; }
    public required string City { get; set; }
    public required string? State { get; set; }
    public required int CountryId { get; set; }
}
