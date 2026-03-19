using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase;

public class MainDbContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Client> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
        modelBuilder.HasAnnotation("Relational:MigrationHistoryTable", "__MainDbHistory");
    }
}