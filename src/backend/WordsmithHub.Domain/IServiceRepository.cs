namespace WordsmithHub.Domain;

public interface IServiceRepository
{
    Task<IReadOnlyList<Service>> GetAllAsync(CancellationToken cancellationToken = default);
}
