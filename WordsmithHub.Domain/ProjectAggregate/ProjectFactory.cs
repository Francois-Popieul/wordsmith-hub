namespace WordsmithHub.Domain.ProjectAggregate;

public interface IProjectFactory
{
    Project CreateProject(
        string name,
        string domain,
        string? description,
        Guid freelanceId,
        Guid directCustomerId,
        Guid? endCustomerId);
}

public class ProjectFactory : IProjectFactory
{
    public Project CreateProject(
        string name,
        string domain,
        string? description,
        Guid freelanceId,
        Guid directCustomerId,
        Guid? endCustomerId
    )

    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = name,
            Domain = domain,
            Description = description ?? string.Empty,
            FreelanceId = freelanceId,
            DirectCustomerId = directCustomerId,
            EndCustomerId = endCustomerId ?? null,
            StatusId = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return project;
    }
}
