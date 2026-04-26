using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using AwesomeAssertions;
using WordsmithHub.API.Features.DirectCustomers.Add;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Tests.DirectCustomers;

internal static class AddDirectCustomerRequestTestHelper
{
    public static AddDirectCustomerRequest CreateValid(Fixture fixture)
    {
        return fixture.Build<AddDirectCustomerRequest>()
            .With(x => x.Name, "ACME Corp")
            .With(x => x.Code, "ACME")
            .With(x => x.Phone, "+33123456789")
            .With(x => x.Email, "billing@acme.test")
            .With(x => x.Address, new Address
            {
                StreetInfo = "ACME Street 1",
                AddressComplement = "ACME Building",
                PostCode = "10000",
                City = "ACME City",
                State = "ACME State",
                CountryId = 1
            })
            .With(x => x.SiretOrSiren, "12345678901234")
            .With(x => x.PaymentDelay, 30)
            .With(x => x.CurrencyId, 1)
            .Create();
    }
}

public class AddDirectCustomerEndpointTests(
    WebApplicationFactoryWithAuthorizedAccess factory,
    ITestContextAccessor testContextAccessor) : IClassFixture<WebApplicationFactoryWithAuthorizedAccess>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly Fixture _fixture = new();
    private readonly CancellationToken _cancellationToken = testContextAccessor.Current.CancellationToken;

    [Fact]
    public async Task AddDirectCustomerEndpoint_ShouldReturn200AndDirectCustomerId_WhenValidRequest()
    {
        // Arrange
        var request = AddDirectCustomerRequestTestHelper.CreateValid(_fixture);

        // Act
        var response = await _client.PostAsJsonAsync("/directcustomer", request, _cancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var actualId = await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: _cancellationToken);
        actualId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task AddDirectCustomerEndpoint_ShouldReturn400_WhenRequestIsInvalid()
    {
        // Arrange
        var request = _fixture.Build<AddDirectCustomerRequest>()
            .With(x => x.Name, string.Empty)
            .With(x => x.Code, "TOOLONG")
            .With(x => x.Email, "not-an-email")
            .With(x => x.Address, (Address)null!)
            .Create();

        // Act
        var response = await _client.PostAsJsonAsync("/directcustomer", request, _cancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

public class AddDirectCustomerForbiddenEndpointTests(
    WebApplicationFactoryWithForbiddenAccess factory,
    ITestContextAccessor testContextAccessor) : IClassFixture<WebApplicationFactoryWithForbiddenAccess>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly Fixture _fixture = new();
    private readonly CancellationToken _cancellationToken = testContextAccessor.Current.CancellationToken;

    [Fact]
    public async Task AddDirectCustomerEndpoint_ShouldReturn403_WhenNoFreelanceProfileExistsForUser()
    {
        // Arrange
        var request = AddDirectCustomerRequestTestHelper.CreateValid(_fixture);

        // Act
        var response = await _client.PostAsJsonAsync("/directcustomer", request, _cancellationToken);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
