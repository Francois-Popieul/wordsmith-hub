using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Delete;

public class DeleteFreelanceHandler(
    IFreelanceAccessService freelanceAccessService,
    IFreelanceRepository repository)
{
    public async Task<OperationResult<Guid>> HandleAsync(Guid appUserId, Guid freelanceId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(appUserId, cancellationToken);

        if (freelance == null || freelance.Id != freelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        freelance.MarkAsDeleted();

        await repository.ArchiveAsync(freelance, cancellationToken);

        return OperationResult.Success(freelance.Id);
    }
}
