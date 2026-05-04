namespace WordsmithHub.Domain;

public interface ILanguageRepository
{
    Task<IReadOnlyList<TranslationLanguage>> GetAllAsync(CancellationToken cancellationToken = default);
}
