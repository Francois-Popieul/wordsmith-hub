using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Get;

public class GetDirectCustomerHandler(
    IFreelanceAccessService freelanceAccessService,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
{
    public async Task<GetResult<DirectCustomer>> HandleAsync(Guid appUserId, Guid directCustomerId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(appUserId, cancellationToken);

        if (freelance == null)
        {
            return new GetResult<DirectCustomer>(GetResultType.Forbidden);
        }

        if (!await resourceAuthorizationService
                .CanAccessAsync<DirectCustomer>(appUserId, directCustomerId, cancellationToken))
        {
            return new GetResult<DirectCustomer>(GetResultType.Forbidden);
        }

        var directCustomer = await repository.GetByIdAsync(directCustomerId, cancellationToken);

        if (directCustomer == null)
        {
            return new GetResult<DirectCustomer>(GetResultType.NotFound);
        }

        return new GetResult<DirectCustomer>(GetResultType.Success, directCustomer);
    }
}
