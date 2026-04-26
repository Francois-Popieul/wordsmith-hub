using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WordsmithHub.Infrastructure.MainDatabase;

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
            .UseNpgsql(connectionString);

        return new MainDbContext(options.Options);
    }
}
