using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WordsmithHub.Infrastructure.IdentityDatabase;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public new virtual DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IdentityDbContext).Assembly,
            type => type.Namespace != null &&
                    type.Namespace.StartsWith(
                        "WordsmithHub.Infrastructure.IdentityDatabase.Configurations",
                        StringComparison.Ordinal));
        modelBuilder.HasAnnotation("Relational:MigrationHistoryTable", "__IdentityDbHistory");
    }
};
