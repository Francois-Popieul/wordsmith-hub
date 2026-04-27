using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.Common;

public abstract class ApiEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected Task SendResult(OperationResult<TResponse> result, CancellationToken ct)
    {
        return HttpContext.MapToHttpAsync(result, ct);
    }
}

public abstract class ApiEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected Task SendResult(OperationResult<object?> result, CancellationToken ct)
    {
        return HttpContext.MapToHttpAsync(result, ct);
    }
}

public abstract class ApiEndpointWithoutRequest<TResponse> : EndpointWithoutRequest<TResponse>
{
    protected Task SendResult(OperationResult<TResponse> result, CancellationToken ct)
    {
        return HttpContext.MapToHttpAsync(result, ct);
    }
}

public abstract class ApiEndpointWithoutRequest : EndpointWithoutRequest
{
    protected Task SendResult<T>(OperationResult<T> result, CancellationToken ct)
    {
        return HttpContext.MapToHttpAsync(result, ct);
    }
}
