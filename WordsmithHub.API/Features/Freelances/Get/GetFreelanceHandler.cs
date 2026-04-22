using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;
using WordsmithHub.API.Features.Freelances.Services;
using WordsmithHub.API.Services.FreelanceAccessService;

namespace WordsmithHub.API.Features.Freelances.Get;

public class GetFreelanceHandler(IFreelanceAccessService freelanceAccessService)
{
    public async Task<OperationResult<FreelanceDto>> HandleAsync(
        Guid userId,
        Guid freelanceId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(userId, cancellationToken);

        if (freelance == null || freelance.Id != freelanceId)
        {
            return OperationResult.Forbidden<FreelanceDto>();
        }

        return OperationResult.Success(freelance.ToDto());
    }
}
