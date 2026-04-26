namespace WordsmithHub.API.Features.Common.Results;

public enum OperationStatus
{
    Success,
    NotFound,
    Forbidden,
    ValidationError,
    Conflict,
    Unauthorized,
    Error
}
