namespace WordsmithHub.Domain;

public interface IDomainTypeRepository
{
    Task<IReadOnlyList<DomainType>> GetAllAsync(CancellationToken cancellationToken = default);
}
