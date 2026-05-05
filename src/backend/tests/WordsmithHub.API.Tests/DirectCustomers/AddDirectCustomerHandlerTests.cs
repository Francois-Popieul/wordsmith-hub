using AutoFixture;
using JetBrains.Annotations;
using Moq;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.DirectCustomers.Add;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Tests.DirectCustomers;

[UsedImplicitly]
public class AddDirectCustomerHandlerTests
{
    private readonly Mock<IFreelanceRepository> _mockFreelanceRepository = new();
    private readonly Mock<IDirectCustomerRepository> _mockDirectCustomerRepository = new();
    private readonly Mock<IDirectCustomerFactory> _mockDirectCustomerFactory = new();
    private readonly Fixture _fixture;
    private readonly AddDirectCustomerCommand _command;
    private readonly DirectCustomer _directCustomer;

    public AddDirectCustomerHandlerTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _command = _fixture.Create<AddDirectCustomerCommand>();
        _directCustomer = _fixture.Create<DirectCustomer>();
        _mockDirectCustomerFactory
            .Setup(x => x.CreateDirectCustomer(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string?>(),
                It.IsAny<string>(),
                It.IsAny<Address>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            ))
            .Returns(_directCustomer);
    }

    private AddDirectCustomerHandler CreateSut()
    {
        return new AddDirectCustomerHandler(_mockFreelanceRepository.Object, _mockDirectCustomerRepository.Object,
            _mockDirectCustomerFactory.Object);
    }

    private void VerifyFactoryCalledOnce()
    {
        _mockDirectCustomerFactory.Verify(
            x => x.CreateDirectCustomer(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string?>(),
                It.IsAny<string>(),
                It.IsAny<Address>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            ),
            Times.Once);
    }

    private void VerifyFactoryCalledNever()
    {
        _mockDirectCustomerFactory.Verify(
            x => x.CreateDirectCustomer(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string?>(),
                It.IsAny<string>(),
                It.IsAny<Address>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()
            ),
            Times.Never);
    }

    private void VerifyRepositoryAddAsyncCalledOnce()
    {
        _mockDirectCustomerRepository.Verify(
            x => x.AddAsync(_directCustomer, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private void VerifyRepositoryAddAsyncCalledNever()
    {
        _mockDirectCustomerRepository.Verify(
            x => x.AddAsync(_directCustomer, It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCreateDirectCustomerAndReturnId_WhenFreelanceExists()
    {
        // Arrange
        var freelance = _fixture.Create<Freelance>();
        _mockFreelanceRepository
            .Setup(x => x.GetByAppUserIdAsync(_command.AppUserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(freelance);

        // Act
        var result = await CreateSut().ExecuteAsync(_command, CancellationToken.None);

        // Assert
        Assert.Equal(OperationStatus.Success, result.Status);
        Assert.Equal(_directCustomer.Id, result.Value);
        VerifyFactoryCalledOnce();
        VerifyRepositoryAddAsyncCalledOnce();
    }

    [Fact]
    public async Task Handle_ShouldReturnForbidden_WhenNoFreelanceExistsForUser()
    {
        // Arrange
        _mockFreelanceRepository
            .Setup(x => x.GetByAppUserIdAsync(_command.AppUserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Freelance?)null);

        // Act
        var result = await CreateSut().ExecuteAsync(_command, CancellationToken.None);

        // Assert
        Assert.Equal(OperationStatus.Forbidden, result.Status);
        VerifyFactoryCalledNever();
        VerifyRepositoryAddAsyncCalledNever();
    }
}
