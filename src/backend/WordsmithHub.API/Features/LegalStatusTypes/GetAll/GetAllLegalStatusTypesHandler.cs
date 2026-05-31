using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.LegalStatusTypes.GetAll;

public record GetAllLegalStatusTypesCommand(Guid AppUserId) : ICommand<OperationResult<IReadOnlyList<LegalStatusType>>>;

[UsedImplicitly]
public class GetAllLegalStatusTypesHandler(
    IFreelanceRepository freelanceRepository,
    ILegalStatusTypeRepository repository)
    : ICommandHandler<GetAllLegalStatusTypesCommand, OperationResult<IReadOnlyList<LegalStatusType>>>
{
    public async Task<OperationResult<IReadOnlyList<LegalStatusType>>> ExecuteAsync(
        GetAllLegalStatusTypesCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return new OperationResult<IReadOnlyList<LegalStatusType>>(OperationStatus.Forbidden);
        }

        var legalStatusTypes = await repository.GetAllAsync(cancellationToken);

        return legalStatusTypes.Count == 0
            ? new OperationResult<IReadOnlyList<LegalStatusType>>(OperationStatus.NotFound)
            : new OperationResult<IReadOnlyList<LegalStatusType>>(OperationStatus.Success, legalStatusTypes);
    }
}
