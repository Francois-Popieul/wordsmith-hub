using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Update;

public record UpdateFreelancePersonalDataCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    Guid AppUserId,
    Guid FreelanceId)
    : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateFreelancePersonalDataHandler(IFreelanceRepository repository)
    : ICommandHandler<UpdateFreelancePersonalDataCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(UpdateFreelancePersonalDataCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await repository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null || freelance.Id != command.FreelanceId)
        {
            return OperationResult.Forbidden<Guid>();
        }

        freelance.FirstName = command.FirstName;
        freelance.LastName = command.LastName;
        freelance.Email = command.Email;
        freelance.Phone = command.Phone;

        await repository.UpdateAsync(freelance, cancellationToken);

        return OperationResult.Success(freelance.Id);
    }
}