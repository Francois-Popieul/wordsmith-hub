using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.FreelanceAccessService;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Update;

public record UpdateFreelanceCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    Address Address,
    Guid AppUserId,
    Guid FreelanceId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateFreelanceHandler(
    IFreelanceAccessService freelanceAccessService,
    IFreelanceFactory freelanceFactory,
    IFreelanceRepository repository)
    : ICommandHandler<UpdateFreelanceCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateFreelanceCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceAccessService.GetFreelanceForUserAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        var updatedFreelance = freelanceFactory.CompleteFreelanceProfile(freelance, command.FirstName, command.LastName,
            command.Email, command.Phone, command.Address);

        await repository.UpdateAsync(updatedFreelance, cancellationToken);

        return OperationResult.Success(updatedFreelance.Id);
    }
}
