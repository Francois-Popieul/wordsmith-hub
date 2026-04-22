using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;

namespace WordsmithHub.API.Features.Freelances.GetAll;

public class GetAllFreelancesEndpoint(GetAllFreelancesHandler handler)
    : EndpointWithoutRequest<IReadOnlyList<FreelanceDto>>
{
    public override void Configure()
    {
        Get("/freelances");
        Roles("admin");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = User.FindFirstValue("sub");
        var appUserRole = User.FindFirstValue("role");

        if (appUserId == null || appUserRole != "admin")
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var result = await handler.HandleAsync(cancellationToken);

        switch (result.Status)
        {
            case OperationStatus.NotFound:
                await Send.NotFoundAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value!, cancellationToken);
                return;
        }
    }
}
