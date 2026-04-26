using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Delete;

public record DeleteFreelanceCommand(Guid AppUserId, Guid FreelanceId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class DeleteFreelanceHandler(
    IFreelanceAccessService freelanceAccessService,
    IFreelanceRepository repository)
    : ICommandHandler<DeleteFreelanceCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(DeleteFreelanceCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        freelance.MarkAsDeleted();

        await repository.ArchiveAsync(freelance, cancellationToken);

        return OperationResult.Success(freelance.Id);
    }
}
