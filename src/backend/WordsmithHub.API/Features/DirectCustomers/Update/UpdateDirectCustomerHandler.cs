using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Update;

public record UpdateDirectCustomerCommand(
    string Name,
    string Code,
    string? Phone,
    string Email,
    Address Address,
    string? SiretOrSiren,
    int PaymentDelay,
    int CurrencyId,
    Guid AppUserId,
    Guid DirectCustomerId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateDirectCustomerHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    IDirectCustomerRepository repository)
    : ICommandHandler<UpdateDirectCustomerCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(
        UpdateDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<DirectCustomer>(command.AppUserId,
                command.DirectCustomerId,
                cancellationToken))
        {
            return OperationResult.Forbidden<Guid>();
        }

        var directCustomer = await repository.GetByIdAsync(command.DirectCustomerId, cancellationToken);

        if (directCustomer == null)
        {
            return OperationResult.NotFound<Guid>();
        }

        directCustomer.Name = command.Name;
        directCustomer.Code = command.Code;
        directCustomer.Phone = command.Phone ?? string.Empty;
        directCustomer.Email = command.Email;
        directCustomer.Address = command.Address;
        directCustomer.SiretOrSiren = command.SiretOrSiren;
        directCustomer.PaymentDelay = command.PaymentDelay;
        directCustomer.CurrencyId = command.CurrencyId;
        directCustomer.UpdatedAt = DateTimeOffset.UtcNow;

        await repository.UpdateAsync(directCustomer, cancellationToken);

        return OperationResult.Success(directCustomer.Id);
    }
}
