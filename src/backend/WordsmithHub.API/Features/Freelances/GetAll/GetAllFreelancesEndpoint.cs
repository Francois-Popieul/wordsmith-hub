using System.Security.Claims;
using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Freelances.Models;

namespace WordsmithHub.API.Features.Freelances.GetAll;

public class GetAllFreelancesEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<FreelanceDto>>
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
        var appUserRole = User.FindFirstValue("role");

        if (appUserRole != "admin")
        {
            await Send.UnauthorizedAsync(cancellationToken);
            return;
        }

        var command = new GetAllFreelancesCommand();

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}