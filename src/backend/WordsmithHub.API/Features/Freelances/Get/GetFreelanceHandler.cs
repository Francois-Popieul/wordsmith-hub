using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;
using WordsmithHub.API.Features.Freelances.Services;
using WordsmithHub.API.Services.FreelanceAccessService;

namespace WordsmithHub.API.Features.Freelances.Get;

public record GetFreelanceCommand(Guid AppUserId) : ICommand<OperationResult<ProfileDto>>;

[UsedImplicitly]
public class GetFreelanceHandler(IFreelanceAccessService freelanceAccessService)
    : ICommandHandler<GetFreelanceCommand, OperationResult<ProfileDto>>
{
    public async Task<OperationResult<ProfileDto>> ExecuteAsync(GetFreelanceCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceProfileAsync(command.AppUserId, cancellationToken);

        return freelance == null
            ? OperationResult.Forbidden<ProfileDto>()
            : OperationResult.Success(freelance.ToDtoProfile());
    }
}
