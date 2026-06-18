namespace WordsmithHub.Domain;

public interface ILegalStatusTypeRepository
{
    Task<IReadOnlyList<LegalStatusType>> GetAllAsync(CancellationToken cancellationToken = default);
}
