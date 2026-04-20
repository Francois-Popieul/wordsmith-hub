namespace WordsmithHub.API.Features.Common.Results;

public record GetResult<T>(GetResultType Type, T? Entity = default);
