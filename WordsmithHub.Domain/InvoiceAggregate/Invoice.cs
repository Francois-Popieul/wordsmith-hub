namespace WordsmithHub.Domain.InvoiceAggregate;

public class Invoice : BaseEntity
{
    public string? InvoiceNumber { get; set; }
    public required decimal TotalAmount { get; set; }
    public required decimal VatAmount { get; set; }
    public required decimal TotalAmountWithVat { get; set; }
    public required Guid WorkOrderId { get; set; }
    public required Guid FreelanceId { get; set; }
    public required Guid DirectCustomerId { get; set; }
    public required DateTime InvoiceDate { get; set; }
    public required DateTime DueDate { get; set; }
    public required DateTime PaymentDate { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerPhone { get; set; }
    public required string CustomerAddress { get; set; }
    public string? CustomerSiretOrSiren { get; set; }
    public required string FreelanceName { get; set; }
    public required string FreelancePhone { get; set; }
    public required string FreelanceAddress { get; set; }
    public string? FreelanceSiret { get; set; }
    public string? FreelanceVatNumber { get; set; }
    public required bool FreelanceVatExemption { get; set; }
    public required bool FreelanceTaxDeductionExemption { get; set; }
    public required string FreelanceBankName { get; set; }
    public required string FreelanceBankAccountHolder { get; set; }
    public required string FreelanceIban { get; set; }
    public required string FreelanceBic { get; set; }
    public required int UsedCurrencyId { get; set; }
    public required int StatusId { get; set; }
}