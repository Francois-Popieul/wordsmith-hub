namespace WordsmithHub.API.Features.Common.Results;

public record OperationResult<T>(OperationStatus Status, T? Value = default);

public static class OperationResult
{
    public static OperationResult<T> Success<T>(T value)
    {
        return new OperationResult<T>(OperationStatus.Success, value);
    }

    public static OperationResult<T> NotFound<T>()
    {
        return new OperationResult<T>(OperationStatus.NotFound);
    }

    public static OperationResult<T> Forbidden<T>()
    {
        return new OperationResult<T>(OperationStatus.Forbidden);
    }

    public static OperationResult<T> Unauthorized<T>()
    {
        return new OperationResult<T>(OperationStatus.Unauthorized);
    }

    public static OperationResult<T> Conflict<T>()
    {
        return new OperationResult<T>(OperationStatus.Conflict);
    }

    public static OperationResult<T> Error<T>()
    {
        return new OperationResult<T>(OperationStatus.Error);
    }

    public static OperationResult<T> ValidationError<T>()
    {
        return new OperationResult<T>(OperationStatus.ValidationError);
    }
}
