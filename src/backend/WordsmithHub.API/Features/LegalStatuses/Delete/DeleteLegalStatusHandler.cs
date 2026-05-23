using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.LegalStatusAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.LegalStatuses.Delete;

public record DeleteLegalStatusCommand(Guid AppUserId, Guid LegalStatusId) : ICommand<OperationResult<NoContent>>;

[UsedImplicitly]
public class DeleteLegalStatusHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    ILegalStatusRepository repository)
    : ICommandHandler<DeleteLegalStatusCommand, OperationResult<NoContent>>
{
    public async Task<OperationResult<NoContent>> ExecuteAsync(DeleteLegalStatusCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<LegalStatus>(command.AppUserId,
                command.LegalStatusId, cancellationToken))
        {
            return OperationResult.Forbidden<NoContent>();
        }

        var legalStatus = await repository.GetByIdAsync(command.LegalStatusId, cancellationToken);

        if (legalStatus == null)
        {
            return OperationResult.NotFound<NoContent>();
        }

        legalStatus.MarkAsDeleted();

        await repository.ArchiveAsync(legalStatus, cancellationToken);

        return OperationResult.Success(new NoContent());
    }
}