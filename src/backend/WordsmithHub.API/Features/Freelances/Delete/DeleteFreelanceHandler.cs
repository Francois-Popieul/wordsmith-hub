using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Delete;

public record DeleteFreelanceCommand(Guid AppUserId, Guid FreelanceId) : ICommand<OperationResult<NoContent>>;

[UsedImplicitly]
public class DeleteFreelanceHandler(
    IFreelanceRepository repository)
    : ICommandHandler<DeleteFreelanceCommand, OperationResult<NoContent>>
{
    public async Task<OperationResult<NoContent>> ExecuteAsync(DeleteFreelanceCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await repository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<NoContent>();
        }

        freelance.MarkAsDeleted();

        await repository.ArchiveAsync(freelance, cancellationToken);

        return OperationResult.Success(new NoContent());
    }
}
