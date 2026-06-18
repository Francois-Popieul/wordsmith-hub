using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WordsmithHub.Infrastructure.IdentityDatabase;

[UsedImplicitly]
public class IdentityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<IdentityDbContextDesignTimeFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("IdentityDbConnection");
        Guard.Against.NullOrWhiteSpace(connectionString);

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__IdentityDbHistory"));

        return new IdentityDbContext(options.Options);
    }
}
