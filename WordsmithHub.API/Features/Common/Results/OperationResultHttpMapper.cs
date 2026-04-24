using FastEndpoints;

namespace WordsmithHub.API.Features.Common.Results;

public static class OperationResultHttpMapper
{
    public static async Task SendResultAsync<T>(
        this HttpContext http,
        OperationResult<T> result,
        CancellationToken cancellationToken = default) where T : notnull
    {
        switch (result.Status)
        {
            case OperationStatus.Success:
                await http.Response.SendOkAsync(result.Value!, cancellation: cancellationToken);
                return;

            case OperationStatus.NotFound:
                await http.Response.SendNotFoundAsync(cancellationToken);
                return;

            case OperationStatus.Forbidden:
                await http.Response.SendForbiddenAsync(cancellationToken);
                return;

            case OperationStatus.Unauthorized:
                await http.Response.SendUnauthorizedAsync(cancellationToken);
                return;

            case OperationStatus.ValidationError:
                await http.Response.SendAsync(
                    new { },
                    StatusCodes.Status400BadRequest,
                    cancellation: cancellationToken
                );
                return;

            case OperationStatus.Conflict:
                await http.Response.SendAsync(
                    new { },
                    StatusCodes.Status409Conflict,
                    cancellation: cancellationToken
                );
                return;

            case OperationStatus.Error:
            default:
                await http.Response.SendAsync(
                    new { },
                    StatusCodes.Status500InternalServerError,
                    cancellation: cancellationToken
                );
                return;
        }
    }
}
