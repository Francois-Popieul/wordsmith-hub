using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Update;

public class UpdateDirectCustomerHandler(
    IFreelanceAccessService freelanceAccessService,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
{
    public async Task<OperationResult<Guid>> HandleAsync(
        UpdateDirectCustomerRequest request,
        Guid userId,
        Guid directCustomerId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(userId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<DirectCustomer>(userId, directCustomerId,
                cancellationToken))
        {
            return OperationResult.Forbidden<Guid>();
        }

        var directCustomer = await repository.GetByIdAsync(directCustomerId, cancellationToken);

        if (directCustomer == null)
        {
            return OperationResult.NotFound<Guid>();
        }

        directCustomer.Name = request.Name;
        directCustomer.Code = request.Code;
        directCustomer.Phone = request.Phone ?? string.Empty;
        directCustomer.Email = request.Email;
        directCustomer.Address = request.Address;
        directCustomer.SiretOrSiren = request.SiretOrSiren;
        directCustomer.PaymentDelay = request.PaymentDelay;
        directCustomer.CurrencyId = request.CurrencyId;
        directCustomer.UpdatedAt = DateTimeOffset.UtcNow;

        await repository.UpdateAsync(directCustomer, cancellationToken);

        return OperationResult.Success(directCustomer.Id);
    }
}
