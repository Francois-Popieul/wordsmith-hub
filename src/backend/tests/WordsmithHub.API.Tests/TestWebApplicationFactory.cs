using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WordsmithHub.Infrastructure.IdentityDatabase;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Tests;

public abstract class TestWebApplicationFactory(string roles) : WebApplicationFactory<Program>
{
    protected Guid AppUserId { get; } = Guid.Parse("11111111-1111-1111-1111-111111111111");

    private string Roles { get; } = roles;
    private string MainDatabaseName { get; } = $"main-db-{Guid.NewGuid()}";
    private string IdentityDatabaseName { get; } = $"identity-db-{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTest");
        builder.ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = "integration-test-jwt-key-with-sufficient-length",
                ["ConnectionStrings:IdentityDbConnection"] =
                    "Host=localhost;Database=identity_test;Username=test;Password=test",
                ["ConnectionStrings:MainDbConnection"] = "Host=localhost;Database=main_test;Username=test;Password=test"
            });
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<MainDbContext>>();
            services.RemoveAll<MainDbContext>();
            services.RemoveAll<DbContextOptions<IdentityDbContext>>();
            services.RemoveAll<IdentityDbContext>();

            services.AddDbContext<MainDbContext>(options =>
                options.UseInMemoryDatabase(MainDatabaseName));
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseInMemoryDatabase(IdentityDatabaseName));

            services.AddFastEndpoints(config =>
            {
                config.Assemblies = [typeof(Program).Assembly];
                config.SourceGeneratorDiscoveredTypes = [];
                config.Filter = type => type.Assembly == typeof(Program).Assembly;
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                    options.DefaultScheme = "Test";
                })
                .AddScheme<TestAuthSchemeOptions, TestAuthHandler>("Test", options =>
                {
                    options.Roles = Roles;
                    options.AppUserId = AppUserId;
                });

            services.PostConfigure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
                options.DefaultScheme = "Test";
            });

            services.AddAuthorization();
        });
    }
}
