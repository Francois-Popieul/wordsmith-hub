using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Features.DirectCustomers.Get;

public record GetDirectCustomerCommand(Guid DirectCustomerId);

public class GetDirectCustomerHandler(Repository<DirectCustomer> repository)
{
    public async Task<DirectCustomer?> HandleAsync(GetDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        return await repository.GetByIdAsync(command.DirectCustomerId, cancellationToken);
    }
}
