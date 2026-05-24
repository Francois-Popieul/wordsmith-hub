using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Features.LegalStatuses.Models;
using WordsmithHub.API.Features.LegalStatuses.Services;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.API.Features.LegalStatuses.GetAll;

[UsedImplicitly]
public record GetAllLegalStatusesCommand(Guid AppUserId) : ICommand<OperationResult<IReadOnlyList<LegalStatusDto>>>;

public class GetAllLegalStatusesHandler(
    IFreelanceRepository freelanceRepository,
    ILegalStatusRepository repository)
    : ICommandHandler<GetAllLegalStatusesCommand, OperationResult<IReadOnlyList<LegalStatusDto>>>
{
    public async Task<OperationResult<IReadOnlyList<LegalStatusDto>>> ExecuteAsync(
        GetAllLegalStatusesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<LegalStatusDto>>(OperationStatus.Forbidden);
        }

        var legalStatuses = await repository.GetByFreelanceIdAsync(freelance.Id, cancellationToken);

        if (legalStatuses.Count == 0)
        {
            return new OperationResult<IReadOnlyList<LegalStatusDto>>(OperationStatus.Success, new List<LegalStatusDto>());
        }

        var customerDtoList = legalStatuses.Select(legalStatus => legalStatus.ToDto()).ToList();

        return new OperationResult<IReadOnlyList<LegalStatusDto>>(OperationStatus.Success, customerDtoList);
    }
}
