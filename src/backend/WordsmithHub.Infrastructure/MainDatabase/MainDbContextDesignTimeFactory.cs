using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WordsmithHub.Infrastructure.MainDatabase;

[UsedImplicitly]
public class MainDbContextDesignTimeFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<MainDbContextDesignTimeFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("MainDbConnection");
        Guard.Against.NullOrWhiteSpace(connectionString);

        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseNpgsql(connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__MainDbHistory"));

        return new MainDbContext(options.Options);
    }
}
