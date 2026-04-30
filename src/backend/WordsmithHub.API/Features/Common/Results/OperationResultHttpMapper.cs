using System.Text.Json;

namespace WordsmithHub.API.Features.Common.Results;

public static class OperationResultHttpMapper
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static async Task MapToHttpAsync<T>(
        this HttpContext httpContext,
        OperationResult<T> result,
        CancellationToken cancellationToken)
    {
        if (typeof(T) == typeof(NoContent))
        {
            httpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return;
        }

        var (statusCode, body) = result.Status switch
        {
            OperationStatus.Success => (StatusCodes.Status200OK, (object?)result.Value),
            OperationStatus.NotFound => (StatusCodes.Status404NotFound, null),
            OperationStatus.Forbidden => (StatusCodes.Status403Forbidden, null),
            OperationStatus.Unauthorized => (StatusCodes.Status401Unauthorized, null),
            OperationStatus.ValidationError => (StatusCodes.Status400BadRequest, null),
            OperationStatus.Conflict => (StatusCodes.Status409Conflict, null),
            _ => (StatusCodes.Status500InternalServerError, null)
        };

        var response = httpContext.Response;
        response.StatusCode = statusCode;

        if (body is not null)
        {
            response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(response.Body, body, JsonOptions, cancellationToken);
        }
    }
}
