using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.DirectCustomers.Models;
using WordsmithHub.API.Features.DirectCustomers.Services;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Get;

public class GetDirectCustomerHandler(
    IFreelanceAccessService freelanceAccessService,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
{
    public async Task<OperationResult<DirectCustomerDto>> HandleAsync(Guid appUserId, Guid directCustomerId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(appUserId, cancellationToken);

        if (freelance == null || !await resourceAuthorizationService
                .CanAccessAsync<DirectCustomer>(appUserId, directCustomerId, cancellationToken))
        {
            return new OperationResult<DirectCustomerDto>(OperationStatus.Forbidden);
        }

        var directCustomer = await repository.GetByIdAsync(directCustomerId, cancellationToken);

        return directCustomer == null
            ? new OperationResult<DirectCustomerDto>(OperationStatus.NotFound)
            : new OperationResult<DirectCustomerDto>(OperationStatus.Success, directCustomer.ToDto());
    }
}
