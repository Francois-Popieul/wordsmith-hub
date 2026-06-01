using AwesomeAssertions;
using Microsoft.AspNetCore.Http;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Tests.Common.Results;

public class OperationResultHttpMapperTests
{
    [Fact]
    public async Task MapToHttpAsync_ShouldReturn204AndNoBody_WhenSuccessfulNoContentResult()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
        var result = OperationResult.Success(new NoContent());

        // Act
        await httpContext.MapToHttpAsync(result, TestContext.Current.CancellationToken);

        // Assert
        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        httpContext.Response.ContentType.Should().BeNull();
        httpContext.Response.Body.Length.Should().Be(0);
    }

    [Fact]
    public async Task MapToHttpAsync_ShouldPreserveForbiddenStatus_WhenNoContentResultIsForbidden()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
        var result = OperationResult.Forbidden<NoContent>();

        // Act
        await httpContext.MapToHttpAsync(result, TestContext.Current.CancellationToken);

        // Assert
        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
        httpContext.Response.ContentType.Should().BeNull();
        httpContext.Response.Body.Length.Should().Be(0);
    }
}