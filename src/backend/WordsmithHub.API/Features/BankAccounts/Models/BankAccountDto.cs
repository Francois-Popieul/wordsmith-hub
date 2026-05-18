namespace WordsmithHub.API.Features.BankAccounts.Models;

public record BankAccountDto
{
    public required Guid Id { get; set; }
    public required string Label { get; set; }
    public required string BankName { get; set; }
    public required string AccountHolderName { get; set; }
    public required string Iban { get; set; }
    public required string Bic { get; set; }
    public bool IsDefault { get; set; }
}
