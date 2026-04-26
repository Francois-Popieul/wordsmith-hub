using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Delete;

public record DeleteDirectCustomerCommand(Guid AppUserId, Guid DirectCustomerId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class DeleteDirectCustomerHandler(
    IFreelanceAccessService freelanceAccessService,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
    : ICommandHandler<DeleteDirectCustomerCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(DeleteDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<DirectCustomer>(command.AppUserId,
                command.DirectCustomerId, cancellationToken))
        {
            return OperationResult.Forbidden<Guid>();
        }

        var directCustomer = await repository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        if (directCustomer == null)
        {
            return OperationResult.NotFound<Guid>();
        }

        directCustomer.MarkAsDeleted();

        await repository.ArchiveAsync(directCustomer, cancellationToken);

        return OperationResult.Success(directCustomer.Id);
    }
}
