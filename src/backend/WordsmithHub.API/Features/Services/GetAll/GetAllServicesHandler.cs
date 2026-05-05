using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Services.GetAll;

public record GetAllServicesCommand(Guid AppUserId)
    : ICommand<OperationResult<IReadOnlyList<Service>>>;

[UsedImplicitly]
public class GetAllServicesHandler(
    IFreelanceRepository freelanceRepository,
    IServiceRepository repository)
    : ICommandHandler<GetAllServicesCommand, OperationResult<IReadOnlyList<Service>>>
{
    public async Task<OperationResult<IReadOnlyList<Service>>> ExecuteAsync(
        GetAllServicesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<Service>>(OperationStatus.Forbidden);
        }

        var services = await repository.GetAllAsync(cancellationToken);

        if (services.Count == 0)
        {
            return new OperationResult<IReadOnlyList<Service>>(OperationStatus.NotFound);
        }

        return new OperationResult<IReadOnlyList<Service>>(OperationStatus.Success, services);
    }
}
