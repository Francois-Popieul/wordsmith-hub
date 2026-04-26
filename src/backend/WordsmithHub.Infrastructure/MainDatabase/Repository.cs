using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase;

public class Repository<T>(MainDbContext context) : IRepository<T>
    where T : BaseEntity
{
    protected readonly MainDbContext Context = context;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Context.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
