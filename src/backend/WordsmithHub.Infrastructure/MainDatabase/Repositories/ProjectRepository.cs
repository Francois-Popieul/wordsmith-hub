using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class ProjectRepository(MainDbContext context) : Repository<Project>(context), IProjectRepository
{
    public async Task<IReadOnlyList<Project>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects.AsNoTracking()
            .Where(p => p.FreelanceId == freelanceId && p.StatusId != StatusIds.General.Inactive)
            .Include(p => p.DirectCustomers)
            .Include(p => p.EndCustomer)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Project>> GetByDirectCustomerIdAsync(Guid directCustomerId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects.AsNoTracking()
            .Where(p => p.DirectCustomers.Any(dc =>
                dc.Id == directCustomerId) && p.StatusId != StatusIds.General.Inactive)
            .Include(p => p.DirectCustomers)
            .Include(p => p.EndCustomer)
            .ToListAsync(cancellationToken);
    }

    public async Task<Guid> UpdateInformationAsync(Project project, CancellationToken cancellationToken = default)
    {
        project.DirectCustomers.Clear();
        foreach (var directCustomer in project.DirectCustomers)
            project.DirectCustomers.Add(directCustomer);
        project.UpdatedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync(cancellationToken);
        return project.Id;
    }

    public async Task UpdateStatusAsync(Project project, CancellationToken cancellationToken = default)
    {
        Context.Entry(project).Property(x => x.StatusId).IsModified = true;
        Context.Entry(project).Property(x => x.UpdatedAt).IsModified = true;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task ArchiveAsync(Project project, CancellationToken cancellationToken = default)
    {
        Context.Entry(project).Property(x => x.StatusId).IsModified = true;
        Context.Entry(project).Property(x => x.UpdatedAt).IsModified = true;
        await Context.SaveChangesAsync(cancellationToken);
    }
}
