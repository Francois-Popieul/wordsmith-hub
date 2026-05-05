using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.Freelances.Models;
using WordsmithHub.API.Features.Freelances.Services;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Get;

public record GetFreelanceCommand(Guid AppUserId) : ICommand<OperationResult<ProfileDto>>;

[UsedImplicitly]
public class GetFreelanceHandler(IFreelanceRepository freelanceRepository)
    : ICommandHandler<GetFreelanceCommand, OperationResult<ProfileDto>>
{
    public async Task<OperationResult<ProfileDto>> ExecuteAsync(GetFreelanceCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetProfileByAppUserIdAsync(command.AppUserId, cancellationToken);

        return freelance == null
            ? OperationResult.Forbidden<ProfileDto>()
            : OperationResult.Success(freelance.ToDtoProfile());
    }
}
