using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Countries.GetAll;

public record GetAllCountriesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<Country>>>;

[UsedImplicitly]
public class GetAllCountriesHandler(
    IFreelanceRepository freelanceRepository,
    ICountryRepository repository)
    : ICommandHandler<GetAllCountriesCommand, OperationResult<IReadOnlyList<Country>>>
{
    public async Task<OperationResult<IReadOnlyList<Country>>> ExecuteAsync(
        GetAllCountriesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<Country>>(OperationStatus.Forbidden);
        }

        var countries = await repository.GetAllAsync(cancellationToken);

        return countries.Count == 0
            ? new OperationResult<IReadOnlyList<Country>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<Country>>(OperationStatus.Success, countries);
    }
}
