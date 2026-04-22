using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Delete;

public class DeleteDirectCustomerHandler(
    IFreelanceAccessService freelanceAccessService,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
{
    public async Task<OperationResult<Guid>> HandleAsync(Guid appUserId, Guid directCustomerId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(appUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<DirectCustomer>(appUserId, directCustomerId,
                cancellationToken))
        {
            return OperationResult.Forbidden<Guid>();
        }

        var directCustomer = await repository.GetByIdAsync(directCustomerId, cancellationToken);

        if (directCustomer == null)
        {
            return OperationResult.NotFound<Guid>();
        }

        directCustomer.MarkAsDeleted();

        await repository.ArchiveAsync(directCustomer, cancellationToken);

        return OperationResult.Success(directCustomer.Id);
    }
}
