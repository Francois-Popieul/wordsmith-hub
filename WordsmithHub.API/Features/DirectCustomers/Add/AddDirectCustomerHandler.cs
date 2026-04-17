using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Features.DirectCustomers.Add;

public class AddDirectCustomerHandler(Repository<Domain.DirectCustomerAggregate.DirectCustomer> repository)
{
    public async Task HandleAsync(AddDirectCustomerRequest request, Guid userId,
        CancellationToken cancellationToken)
    {
        var directCustomer = new Domain.DirectCustomerAggregate.DirectCustomer()
        {
            Id = Guid.NewGuid(),
            FreelanceId = userId,
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            PaymentDelay = request.PaymentDelay,
            Code = request.Code,
            CurrencyId = 0,
            StatusId = 0,
            CreatedAt = default,
            UpdatedAt = default
        };

        await repository.AddAsync(directCustomer, cancellationToken);
    }
}