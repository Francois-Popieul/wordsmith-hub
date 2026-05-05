using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Delete;

public record DeleteDirectCustomerCommand(Guid AppUserId, Guid DirectCustomerId)
    : ICommand<OperationResult<NoContent>>;

[UsedImplicitly]
public class DeleteDirectCustomerHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
    : ICommandHandler<DeleteDirectCustomerCommand, OperationResult<NoContent>>
{
    public async Task<OperationResult<NoContent>> ExecuteAsync(DeleteDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<DirectCustomer>(command.AppUserId,
                command.DirectCustomerId, cancellationToken))
        {
            return OperationResult.Forbidden<NoContent>();
        }

        var directCustomer = await repository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        if (directCustomer == null)
        {
            return OperationResult.NotFound<NoContent>();
        }

        directCustomer.MarkAsDeleted();

        await repository.ArchiveAsync(directCustomer, cancellationToken);

        return OperationResult.Success(new NoContent());
    }
}
