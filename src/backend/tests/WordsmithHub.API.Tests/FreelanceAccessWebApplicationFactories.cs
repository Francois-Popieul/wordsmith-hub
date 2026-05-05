using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Tests;

[UsedImplicitly]
public class WebApplicationFactoryWithAuthorizedAccess() : TestWebApplicationFactory("user")
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IFreelanceRepository>();
            services.AddScoped(_ => CreateMockedFreelanceRepository(factoryReturnsFreelance: true, AppUserId));
        });
    }

    public static IFreelanceRepository CreateMockedFreelanceRepository(bool factoryReturnsFreelance, Guid appUserId)
    {
        var freelance = factoryReturnsFreelance
            ? new Freelance
            {
                Id = Guid.NewGuid(),
                AppUserId = appUserId,
                FirstName = "Integration",
                LastName = "Tester",
                Email = "endpoint-test@wordsmithhub.local",
                StatusId = StatusIds.General.Active,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }
            : null;

        var mock = new Mock<IFreelanceRepository>();
        mock.Setup(x => x.GetByAppUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(freelance);
        mock.Setup(x => x.GetProfileByAppUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(freelance);

        return mock.Object;
    }
}

[UsedImplicitly]
public class WebApplicationFactoryWithForbiddenAccess() : TestWebApplicationFactory("user")
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IFreelanceRepository>();
            services.AddScoped(_ => WebApplicationFactoryWithAuthorizedAccess
                .CreateMockedFreelanceRepository(factoryReturnsFreelance: false, AppUserId));
        });
    }
}