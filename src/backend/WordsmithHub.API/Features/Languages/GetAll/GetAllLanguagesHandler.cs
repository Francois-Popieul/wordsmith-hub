using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Languages.GetAll;

public record GetAllLanguagesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<TranslationLanguage>>>;

[UsedImplicitly]
public class GetAllLanguagesHandler(
    IFreelanceRepository freelanceRepository,
    ILanguageRepository repository)
    : ICommandHandler<GetAllLanguagesCommand, OperationResult<IReadOnlyList<TranslationLanguage>>>
{
    public async Task<OperationResult<IReadOnlyList<TranslationLanguage>>> ExecuteAsync(
        GetAllLanguagesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
            return new OperationResult<IReadOnlyList<TranslationLanguage>>(OperationStatus.Forbidden);

        var translationLanguages = await repository.GetAllAsync(cancellationToken);

        return translationLanguages.Count == 0
            ? new OperationResult<IReadOnlyList<TranslationLanguage>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<TranslationLanguage>>(OperationStatus.Success, translationLanguages);
    }
}
