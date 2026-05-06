using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Update;

public record UpdateFreelanceLanguagesCommand(
    IReadOnlyList<int> SourceLanguageIds,
    IReadOnlyList<int> TargetLanguageIds,
    Guid AppUserId,
    Guid FreelanceId)
    : ICommand<OperationResult<Guid>>;

public class UpdateFreelanceLanguagesHandler(IFreelanceRepository repository)
    : ICommandHandler<UpdateFreelanceLanguagesCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateFreelanceLanguagesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance =
            await repository.GetFreelanceWithLanguagesByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        await repository.UpdateLanguagesAsync(freelance, command.SourceLanguageIds, command.TargetLanguageIds,
            cancellationToken);

        return OperationResult.Success(freelance.Id);
    }
}
