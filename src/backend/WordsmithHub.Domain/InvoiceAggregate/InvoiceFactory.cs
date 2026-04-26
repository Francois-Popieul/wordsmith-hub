namespace WordsmithHub.Domain.InvoiceAggregate;

public interface IInvoiceFactory
{
    Invoice CreateInvoice(
        string? invoiceNumber,
        decimal totalAmount,
        decimal vatAmount,
        decimal totalAmountWithVat,
        Guid workOrderId,
        Guid freelanceId,
        Guid directCustomerId,
        DateTime invoiceDate,
        DateTime dueDate,
        DateTime paymentDate,
        string customerName,
        string customerPhone,
        string customerAddress,
        string? customerSiretOrSiren,
        string freelanceName,
        string freelancePhone,
        string freelanceAddress,
        string? freelanceSiret,
        string? freelanceVatNumber,
        bool freelanceVatExemption,
        bool freelanceTaxDeductionExemption,
        string freelanceBankName,
        string freelanceBankAccountHolder,
        string freelanceIban,
        string freelanceBic,
        int usedCurrencyId,
        int statusId
    );
}

public class InvoiceFactory : IInvoiceFactory
{
    public Invoice CreateInvoice(
        string? invoiceNumber,
        decimal totalAmount,
        decimal vatAmount,
        decimal totalAmountWithVat,
        Guid workOrderId,
        Guid freelanceId,
        Guid directCustomerId,
        DateTime invoiceDate,
        DateTime dueDate,
        DateTime paymentDate,
        string customerName,
        string customerPhone,
        string customerAddress,
        string? customerSiretOrSiren,
        string freelanceName,
        string freelancePhone,
        string freelanceAddress,
        string? freelanceSiret,
        string? freelanceVatNumber,
        bool freelanceVatExemption,
        bool freelanceTaxDeductionExemption,
        string freelanceBankName,
        string freelanceBankAccountHolder,
        string freelanceIban,
        string freelanceBic,
        int usedCurrencyId,
        int statusId)

    {
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            InvoiceNumber = invoiceNumber ?? string.Empty,
            TotalAmount = totalAmount,
            VatAmount = vatAmount,
            TotalAmountWithVat = totalAmountWithVat,
            WorkOrderId = workOrderId,
            FreelanceId = freelanceId,
            DirectCustomerId = directCustomerId,
            InvoiceDate = invoiceDate,
            DueDate = dueDate,
            PaymentDate = paymentDate,
            CustomerName = customerName,
            CustomerPhone = customerPhone,
            CustomerAddress = customerAddress,
            CustomerSiretOrSiren = customerSiretOrSiren ?? string.Empty,
            FreelanceName = freelanceName,
            FreelancePhone = freelancePhone,
            FreelanceAddress = freelanceAddress,
            FreelanceSiret = freelanceSiret ?? string.Empty,
            FreelanceVatNumber = freelanceVatNumber ?? string.Empty,
            FreelanceVatExemption = freelanceVatExemption,
            FreelanceTaxDeductionExemption = freelanceTaxDeductionExemption,
            FreelanceBankName = freelanceBankName,
            FreelanceBankAccountHolder = freelanceBankAccountHolder,
            FreelanceIban = freelanceIban,
            FreelanceBic = freelanceBic,
            UsedCurrencyId = usedCurrencyId,
            StatusId = statusId,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return invoice;
    }
}
