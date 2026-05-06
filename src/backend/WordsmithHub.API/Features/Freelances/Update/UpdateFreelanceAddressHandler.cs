using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Update;

public record UpdateFreelanceAddressCommand(
    Address Address,
    Guid AppUserId,
    Guid FreelanceId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateFreelanceAddressHandler(
    IFreelanceRepository repository)
    : ICommandHandler<UpdateFreelanceAddressCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateFreelanceAddressCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await repository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        freelance.Address = command.Address;

        await repository.UpdateAsync(freelance, cancellationToken);

        return OperationResult.Success(freelance.Id);
    }
}