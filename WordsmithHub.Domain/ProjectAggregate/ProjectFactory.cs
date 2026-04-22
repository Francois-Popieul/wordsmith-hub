using WordsmithHub.Domain.DirectCustomerAggregate;

namespace WordsmithHub.Domain.ProjectAggregate;

public interface IProjectFactory
{
    Project CreateProject(
        string name,
        string domain,
        string? description,
        Guid freelanceId,
        Guid? endCustomerId,
        ICollection<DirectCustomer> directCustomers);
}

public class ProjectFactory : IProjectFactory
{
    public Project CreateProject(
        string name,
        string domain,
        string? description,
        Guid freelanceId,
        Guid? endCustomerId,
        ICollection<DirectCustomer> directCustomers
    )

    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = name,
            Domain = domain,
            Description = description ?? string.Empty,
            FreelanceId = freelanceId,
            EndCustomerId = endCustomerId ?? null,
            StatusId = StatusIds.General.Active,
            DirectCustomers = directCustomers,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return project;
    }
}
