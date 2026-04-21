using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.DirectCustomers.Models;
using WordsmithHub.API.Features.DirectCustomers.Services;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.GetAll;

public class GetAllDirectCustomersHandler(
    IFreelanceAccessService freelanceAccessService,
    IDirectCustomerRepository repository)
{
    public async Task<OperationResult<IReadOnlyList<DirectCustomerDto>>> HandleAsync(Guid appUserId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(appUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<DirectCustomerDto>>(OperationStatus.Forbidden);
        }

        var directCustomers = await repository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (directCustomers.Count == 0)
        {
            return new OperationResult<IReadOnlyList<DirectCustomerDto>>(OperationStatus.NotFound);
        }

        var directCustomersDto = directCustomers.Select(directCustomer => directCustomer.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<DirectCustomerDto>>(OperationStatus.Success, directCustomersDto);
    }
}