using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.DirectCustomers.Models;
using WordsmithHub.API.Features.DirectCustomers.Services;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.GetAll;

public record GetAllDirectCustomersCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<DirectCustomerDto>>>;

[UsedImplicitly]
public class GetAllDirectCustomersHandler(
    IFreelanceAccessService freelanceAccessService,
    IDirectCustomerRepository repository)
    : ICommandHandler<GetAllDirectCustomersCommand, OperationResult<IReadOnlyList<DirectCustomerDto>>>
{
    public async Task<OperationResult<IReadOnlyList<DirectCustomerDto>>> ExecuteAsync(
        GetAllDirectCustomersCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<DirectCustomerDto>>(OperationStatus.Forbidden);
        }

        var directCustomers = await repository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (directCustomers.Count == 0)
        {
            return new OperationResult<IReadOnlyList<DirectCustomerDto>>(OperationStatus.NotFound);
        }

        var customerDtoList = directCustomers.Select(directCustomer => directCustomer.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<DirectCustomerDto>>(OperationStatus.Success, customerDtoList);
    }
}
