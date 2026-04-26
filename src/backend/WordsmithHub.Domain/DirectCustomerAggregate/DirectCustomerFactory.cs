namespace WordsmithHub.Domain.DirectCustomerAggregate;

public interface IDirectCustomerFactory
{
    DirectCustomer CreateDirectCustomer(
        Guid freelanceId,
        string name,
        string code,
        string? phone,
        string email,
        Address address,
        string? siretOrSiren,
        int paymentDelay,
        int currencyId);
}

public class DirectCustomerFactory : IDirectCustomerFactory
{
    public DirectCustomer CreateDirectCustomer(
        Guid freelanceId,
        string name,
        string code,
        string? phone,
        string email,
        Address address,
        string? siretOrSiren,
        int paymentDelay,
        int currencyId)
    {
        var directCustomer = new DirectCustomer
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Phone = phone ?? string.Empty,
            Email = email,
            Address = address,
            SiretOrSiren = siretOrSiren ?? string.Empty,
            PaymentDelay = paymentDelay,
            FreelanceId = freelanceId,
            CurrencyId = currencyId,
            StatusId = StatusIds.General.Active,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return directCustomer;
    }
}
