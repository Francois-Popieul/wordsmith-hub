using WordsmithHub.API.Features.Common.Models;

namespace WordsmithHub.API.Features.DirectCustomers.Models;

public record DirectCustomerDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Phone { get; set; }
    public required string Email { get; set; }
    public required AddressDto Address { get; set; }
    public string? SiretOrSiren { get; set; }
    public required int PaymentDelay { get; set; }
    public required int CurrencyId { get; set; }
    public required int StatusId { get; set; }
}
