using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using WordsmithHub.API.Services.FreelanceAccessService;
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
            services.RemoveAll<IFreelanceAccessService>();
            services.AddScoped(_ => CreateMockedFreelanceAccessService(factoryReturnsFreelance: true, AppUserId));
        });
    }

    public static IFreelanceAccessService CreateMockedFreelanceAccessService(bool factoryReturnsFreelance, Guid appUserId)
    {
        var mock = new Mock<IFreelanceAccessService>();
        mock.Setup(x => x.GetFreelanceForUserAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(factoryReturnsFreelance
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
                : null);

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
            services.RemoveAll<IFreelanceAccessService>();
            services.AddScoped(_ => WebApplicationFactoryWithAuthorizedAccess
                .CreateMockedFreelanceAccessService(factoryReturnsFreelance: false, AppUserId));
        });
    }
}