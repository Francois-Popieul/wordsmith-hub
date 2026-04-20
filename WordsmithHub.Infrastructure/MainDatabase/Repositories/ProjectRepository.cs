using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(MainDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Project>> GetByFreelanceIdAsync(Guid freelanceId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects.AsNoTracking()
            .Where(p => p.FreelanceId == freelanceId)
            .ToListAsync(cancellationToken);
    }
}
