using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.DirectCustomers.GetAll;

public record GetAllDirectCustomersForAutocompleteCommand(string UserInput, Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<string>>>;

[UsedImplicitly]
public class GetAllDirectCustomersForAutocompleteHandler(
    IFreelanceRepository freelanceRepository,
    IDirectCustomerRepository repository)
    : ICommandHandler<GetAllDirectCustomersForAutocompleteCommand, OperationResult<IReadOnlyList<string>>>
{
    public async Task<OperationResult<IReadOnlyList<string>>> ExecuteAsync(
        GetAllDirectCustomersForAutocompleteCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<string>>(OperationStatus.Forbidden);

        var directCustomers = await repository.GetAllByUserInputAsync(command.UserInput, cancellationToken);

        if (directCustomers.Count == 0)
            return new OperationResult<IReadOnlyList<string>>(OperationStatus.Success, []);

        var directCustomerList = directCustomers
            .Select(directCustomer => directCustomer.Name)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        return new OperationResult<IReadOnlyList<string>>(OperationStatus.Success, directCustomerList);
    }
}
