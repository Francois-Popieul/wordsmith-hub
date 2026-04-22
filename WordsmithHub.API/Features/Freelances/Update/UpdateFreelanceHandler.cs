using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Update;

public class UpdateFreelanceHandler(
    IFreelanceAccessService freelanceAccessService,
    IFreelanceFactory freelanceFactory,
    IFreelanceRepository repository)
{
    public async Task<OperationResult<Guid>> HandleAsync(
        UpdateFreelanceRequest request,
        Guid userId,
        Guid freelanceId,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(userId, cancellationToken);

        if (freelance == null || freelance.Id != freelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        var updatedFreelance = freelanceFactory.CompleteFreelanceProfile(freelance, request.FirstName, request.LastName,
            request.Email,
            request.Phone, request.Address);

        await repository.UpdateAsync(updatedFreelance, cancellationToken);

        return OperationResult.Success(updatedFreelance.Id);
    }
}
