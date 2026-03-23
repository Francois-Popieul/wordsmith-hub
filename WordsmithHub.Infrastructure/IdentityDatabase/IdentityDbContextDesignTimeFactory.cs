using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WordsmithHub.Infrastructure.IdentityDatabase;

public class IdentityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<IdentityDbContextDesignTimeFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("IdentityDbConnection");
        Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(connectionString);

        return new IdentityDbContext(options.Options);
    }
}
