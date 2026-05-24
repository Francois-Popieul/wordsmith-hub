using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class ProjectRepository(MainDbContext context) : Repository<Project>(context), IProjectRepository
{
    public async Task<IReadOnlyList<Project>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects.AsNoTracking()
            .Where(p => p.FreelanceId == freelanceId)
            .Include(p => p.DirectCustomers)
            .Include(p => p.EndCustomer)
            .ToListAsync(cancellationToken);
    }

    public async Task ArchiveAsync(Project project, CancellationToken cancellationToken = default)
    {
        Context.Entry(project).Property(x => x.StatusId).IsModified = true;
        Context.Entry(project).Property(x => x.UpdatedAt).IsModified = true;
        await Context.SaveChangesAsync(cancellationToken);
    }
}
