using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Add;

public class AddDirectCustomerHandler(
    IFreelanceAccessService freelanceAccessService,
    IDirectCustomerRepository repository,
    IDirectCustomerFactory factory)
{
    public async Task<Guid?> HandleAsync(AddDirectCustomerRequest request, Guid userId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(userId, cancellationToken);

        if (freelance == null)
        {
            return null;
        }

        var directCustomer = factory.CreateDirectCustomer(
            freelance.Id,
            request.Name,
            request.Code,
            request.Phone ?? string.Empty,
            request.Email,
            request.Address,
            request.SiretOrSiren,
            request.PaymentDelay,
            request.CurrencyId);

        await repository.AddAsync(directCustomer, cancellationToken);

        return directCustomer.Id;
    }
}
