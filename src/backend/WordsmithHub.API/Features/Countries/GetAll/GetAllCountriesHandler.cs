using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Countries.GetAll;

public record GetAllCountriesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<Country>>>;

[UsedImplicitly]
public class GetAllCountriesHandler(
    IFreelanceAccessService freelanceAccessService,
    ICountryRepository repository)
    : ICommandHandler<GetAllCountriesCommand, OperationResult<IReadOnlyList<Country>>>
{
    public async Task<OperationResult<IReadOnlyList<Country>>> ExecuteAsync(
        GetAllCountriesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<Country>>(OperationStatus.Forbidden);
        }

        var countries = await repository.GetAllAsync(cancellationToken);

        if (countries.Count == 0)
        {
            return new OperationResult<IReadOnlyList<Country>>(OperationStatus.NotFound);
        }

        return new OperationResult<IReadOnlyList<Country>>(OperationStatus.Success, countries);
    }
}
