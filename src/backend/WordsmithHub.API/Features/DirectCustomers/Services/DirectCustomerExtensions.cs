using WordsmithHub.API.Features.DirectCustomers.Models;
using WordsmithHub.API.Services;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Services;

public static class DirectCustomerExtensions
{
    public static DirectCustomerDto ToDto(this DirectCustomer directCustomer)
    {
        ArgumentNullException.ThrowIfNull(directCustomer);

        return new DirectCustomerDto
        {
            Id = directCustomer.Id,
            Name = directCustomer.Name,
            Code = directCustomer.Code,
            Phone = directCustomer.Phone,
            Email = directCustomer.Email,
            Address = directCustomer.Address.ToDto(),
            SiretOrSiren = directCustomer.SiretOrSiren,
            PaymentDelay = directCustomer.PaymentDelay,
            CurrencyId = directCustomer.CurrencyId,
            StatusId = directCustomer.StatusId
        };
    }
}
