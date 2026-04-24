using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;
using WordsmithHub.API.Features.Freelances.Services;
using WordsmithHub.API.Services.FreelanceAccessService;

namespace WordsmithHub.API.Features.Freelances.Get;

public record GetFreelanceCommand(Guid AppUserId, Guid FreelanceId) : ICommand<OperationResult<FreelanceDto>>;

[UsedImplicitly]
public class GetFreelanceHandler(IFreelanceAccessService freelanceAccessService)
    : ICommandHandler<GetFreelanceCommand, OperationResult<FreelanceDto>>
{
    public async Task<OperationResult<FreelanceDto>> ExecuteAsync(GetFreelanceCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<FreelanceDto>();
        }

        return OperationResult.Success(freelance.ToDto());
    }
}
