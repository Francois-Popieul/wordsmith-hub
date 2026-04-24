using AutoFixture;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Tests.Services;

[UsedImplicitly]
public class ResourceAuthorizationServiceTests : IDisposable
{
    private readonly MainDbContext _context;
    private readonly Fixture _fixture;

    public ResourceAuthorizationServiceTests()
    {
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new MainDbContext(options);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    private ResourceAuthorizationService CreateSut()
    {
        return new ResourceAuthorizationService(_context);
    }

    [Fact]
    public async Task ResourceAuthorizationService_ShouldReturnTrue_WhenUserHasAccessToResource()
    {
        // Arrange
        var freelance = _fixture.Build<Freelance>()
            .Without(f => f.Status)
            .Without(f => f.Address)
            .Without(f => f.SourceLanguages)
            .Without(f => f.TargetLanguages)
            .Without(f => f.Services)
            .Create();
        _context.Freelances.Add(freelance);

        var directCustomer = _fixture.Build<DirectCustomer>()
            .With(c => c.FreelanceId, freelance.Id)
            .Without(c => c.Freelance)
            .Without(c => c.Currency)
            .Without(c => c.Status)
            .Without(c => c.Projects)
            .Create();
        _context.DirectCustomers.Add(directCustomer);

        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var result = await CreateSut().CanAccessAsync<DirectCustomer>(freelance.AppUserId, directCustomer.Id,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ResourceAuthorizationService_ShouldReturnFalse_WhenUserHasNotAccessToResource()
    {
        // Arrange
        var freelance = _fixture.Build<Freelance>()
            .Without(f => f.Status)
            .Without(f => f.Address)
            .Without(f => f.SourceLanguages)
            .Without(f => f.TargetLanguages)
            .Without(f => f.Services)
            .Create();
        _context.Freelances.Add(freelance);

        var directCustomer = _fixture.Build<DirectCustomer>()
            .With(c => c.FreelanceId, Guid.NewGuid())
            .Without(c => c.Freelance)
            .Without(c => c.Currency)
            .Without(c => c.Status)
            .Without(c => c.Projects)
            .Create();
        _context.DirectCustomers.Add(directCustomer);

        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var result = await CreateSut().CanAccessAsync<DirectCustomer>(freelance.AppUserId, directCustomer.Id,
            TestContext.Current.CancellationToken);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ResourceAuthorizationService_ShouldReturnFalse_WhenFreelanceDoesNotExist()
    {
        // Arrange
        var directCustomer = _fixture.Build<DirectCustomer>()
            .With(c => c.FreelanceId, Guid.NewGuid())
            .Without(c => c.Freelance)
            .Without(c => c.Currency)
            .Without(c => c.Status)
            .Without(c => c.Projects)
            .Create();
        _context.DirectCustomers.Add(directCustomer);

        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var result = await CreateSut()
            .CanAccessAsync<DirectCustomer>(Guid.NewGuid(), directCustomer.Id, TestContext.Current.CancellationToken);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ResourceAuthorizationService_ShouldReturnFalse_WhenResourceDoesNotExist()
    {
        // Arrange
        var freelance = _fixture.Build<Freelance>()
            .Without(f => f.Status)
            .Without(f => f.Address)
            .Without(f => f.SourceLanguages)
            .Without(f => f.TargetLanguages)
            .Without(f => f.Services)
            .Create();
        _context.Freelances.Add(freelance);

        await _context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var result = await CreateSut().CanAccessAsync<DirectCustomer>(freelance.AppUserId, Guid.NewGuid(),
            TestContext.Current.CancellationToken);

        // Assert
        Assert.False(result);
    }
}
