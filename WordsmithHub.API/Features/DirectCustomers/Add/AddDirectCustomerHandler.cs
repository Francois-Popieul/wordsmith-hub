using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Features.DirectCustomers.Add;

public class AddDirectCustomerHandler(
    MainDbContext context,
    IDirectCustomerRepository repository,
    IDirectCustomerFactory factory)
{
    public async Task<Guid?> HandleAsync(AddDirectCustomerRequest request, Guid userId,
        CancellationToken cancellationToken)
    {
        var freelance = await context.Freelances.SingleOrDefaultAsync(f => f.AppUserId == userId, cancellationToken);

        if (freelance == null)
        {
            return null;
        }

        var newDirectCustomer = factory.CreateDirectCustomer(
            freelance.Id,
            request.Name,
            request.Code,
            request.Phone,
            request.Email,
            request.Address,
            request.SiretOrSiren,
            request.PaymentDelay,
            request.CurrencyId);

        await repository.AddAsync(newDirectCustomer, cancellationToken);

        return newDirectCustomer.Id;
    }
}
