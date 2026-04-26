using System.Security.Claims;
using FastEndpoints;

namespace WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

public class AppUserIdPreProcessor<TRequest> : IPreProcessor<TRequest>
{
    public Task PreProcessAsync(IPreProcessorContext<TRequest> context, CancellationToken ct)
    {
        var appUserId = context.HttpContext.User.FindFirstValue("sub");

        if (appUserId is null)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }

        context.HttpContext.Items[HttpContextItemKeys.AppUserId] = Guid.Parse(appUserId);
        return Task.CompletedTask;
    }
}
