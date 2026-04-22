using FastEndpoints;
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
}
