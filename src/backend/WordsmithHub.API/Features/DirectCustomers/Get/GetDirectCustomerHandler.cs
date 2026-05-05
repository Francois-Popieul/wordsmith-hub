using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.DirectCustomers.Models;
using WordsmithHub.API.Features.DirectCustomers.Services;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Get;

public record GetDirectCustomerCommand(Guid AppUserId, Guid DirectCustomerId)
    : ICommand<OperationResult<DirectCustomerDto>>;

[UsedImplicitly]
public class GetDirectCustomerHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
    : ICommandHandler<GetDirectCustomerCommand, OperationResult<DirectCustomerDto>>
{
    public async Task<OperationResult<DirectCustomerDto>> ExecuteAsync(GetDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || !await resourceAuthorizationService
                .CanAccessAsync<DirectCustomer>(command.AppUserId, command.DirectCustomerId, cancellationToken))
        {
            return new OperationResult<DirectCustomerDto>(OperationStatus.Forbidden);
        }

        var directCustomer = await repository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        return directCustomer == null
            ? new OperationResult<DirectCustomerDto>(OperationStatus.NotFound)
            : new OperationResult<DirectCustomerDto>(OperationStatus.Success, directCustomer.ToDto());
    }
}
