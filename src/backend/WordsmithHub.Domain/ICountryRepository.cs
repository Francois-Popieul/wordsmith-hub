namespace WordsmithHub.Domain;

public interface ICountryRepository
{
    Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default);
}
