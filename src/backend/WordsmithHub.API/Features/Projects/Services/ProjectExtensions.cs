using WordsmithHub.API.Features.DirectCustomers.Services;
using WordsmithHub.API.Features.Projects.Models;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.API.Features.Projects.Services;

public static class ProjectExtensions
{
    public static ProjectDto ToDto(this Project project)
    {
        ArgumentNullException.ThrowIfNull(project);

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Domain = project.Domain,
            Description = project.Description,
            EndCustomer = project.EndCustomer is null ? null : new EndCustomerDto
            {
                Id = project.EndCustomer.Id,
                Name = project.EndCustomer.Name,
                StatusId = project.EndCustomer.StatusId,
            },
            DirectCustomers = project.DirectCustomers.Select(dc => dc.ToDto()).ToList(),
            StatusId = project.StatusId,
        };
    }
}
