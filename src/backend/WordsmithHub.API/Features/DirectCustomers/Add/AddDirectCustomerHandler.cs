using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.Add;

public record AddDirectCustomerCommand(
    string Name,
    string Code,
    string? Phone,
    string Email,
    Address Address,
    string? SiretOrSiren,
    int PaymentDelay,
    int CurrencyId,
    Guid AppUserId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class AddDirectCustomerHandler(
    IFreelanceRepository freelanceRepository,
    IDirectCustomerRepository repository,
    IDirectCustomerFactory factory) : ICommandHandler<AddDirectCustomerCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(AddDirectCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return OperationResult.Forbidden<Guid>();
        }

        var directCustomer = factory.CreateDirectCustomer(
            freelance.Id,
            command.Name,
            command.Code,
            command.Phone ?? string.Empty,
            command.Email,
            command.Address,
            command.SiretOrSiren,
            command.PaymentDelay,
            command.CurrencyId);

        await repository.AddAsync(directCustomer, cancellationToken);

        return OperationResult.Success(directCustomer.Id);
    }
}
