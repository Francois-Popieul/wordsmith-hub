namespace WordsmithHub.Domain.ProjectAggregate;

public interface IProjectRepository : IRepository<Project>
{
    Task<IReadOnlyList<Project>> GetByFreelanceIdAsync(
        Guid freelanceId,
        CancellationToken cancellationToken = default);

    Task UpdateStatusAsync(Project project, CancellationToken cancellationToken = default);

    Task ArchiveAsync(Project project, CancellationToken cancellationToken = default);
}
