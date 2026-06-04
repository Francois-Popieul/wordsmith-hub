using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.DomainTypes.GetAll;

public record GetAllDomainTypesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<DomainType>>>;

[UsedImplicitly]
public class GetAllCountriesHandler(
    IFreelanceRepository freelanceRepository,
    IDomainTypeRepository repository)
    : ICommandHandler<GetAllDomainTypesCommand, OperationResult<IReadOnlyList<DomainType>>>
{
    public async Task<OperationResult<IReadOnlyList<DomainType>>> ExecuteAsync(
        GetAllDomainTypesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<DomainType>>(OperationStatus.Forbidden);
        }

        var countries = await repository.GetAllAsync(cancellationToken);

        return countries.Count == 0
            ? new OperationResult<IReadOnlyList<DomainType>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<DomainType>>(OperationStatus.Success, countries);
    }
}