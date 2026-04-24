using AutoFixture;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Infrastructure.MainDatabase;

namespace WordsmithHub.API.Tests.Services;

[UsedImplicitly]
public class FreelanceAccessServiceTests : IDisposable
{
    private readonly MainDbContext _context;
    private readonly Fixture _fixture;

    public FreelanceAccessServiceTests()
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

    public void Dispose() => _context.Dispose();

    private FreelanceAccessService CreateSut()
    {
        return new FreelanceAccessService(_context);
    }

    [Fact]
    public async Task GetFreelanceForUserAsync_ShouldReturnFreelance_WhenFreelanceExistsForUser()
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
        var result = await CreateSut().GetFreelanceForUserAsync(freelance.AppUserId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(freelance.Id, result.Id);
    }

    [Fact]
    public async Task GetFreelanceForUserAsync_ShouldReturnNull_WhenNoFreelanceExistsForUser()
    {
        // Arrange
        var appUserId = _fixture.Create<Guid>();

        // Act
        var result = await CreateSut().GetFreelanceForUserAsync(appUserId, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}
