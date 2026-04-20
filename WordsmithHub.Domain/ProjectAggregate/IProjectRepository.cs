namespace WordsmithHub.Domain.ProjectAggregate;

public interface IProjectRepository : IRepository<Project>
{
    Task<IReadOnlyList<Project>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);
}
