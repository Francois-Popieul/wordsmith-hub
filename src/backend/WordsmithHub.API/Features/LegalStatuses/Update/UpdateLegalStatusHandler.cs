using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.API.Services.ResourceAccessService;
using WordsmithHub.Domain.LegalStatusAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.LegalStatuses.Update;

public record UpdateLegalStatusCommand(
    Guid AppUserId,
    Guid LegalStatusId,
    string Name,
    string? Siret,
    string? VatNumber,
    bool VatExemption,
    decimal? VatRate,
    bool TaxDeductionExemption,
    DateTimeOffset ValidFrom,
    DateTimeOffset? ValidTo) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class UpdateLegalStatusHandler(
    IFreelanceRepository freelanceRepository,
    IResourceAuthorizationService resourceAuthorizationService,
    ILegalStatusRepository repository)
    : ICommandHandler<UpdateLegalStatusCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(
        UpdateLegalStatusCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null ||
            !await resourceAuthorizationService.CanAccessAsync<LegalStatus>(command.AppUserId,
                command.LegalStatusId,
                cancellationToken))
        {
            return OperationResult.Forbidden<Guid>();
        }

        var legalStatus = await repository.GetByIdAsync(command.LegalStatusId, cancellationToken);

        if (legalStatus == null)
        {
            return OperationResult.NotFound<Guid>();
        }

        legalStatus.Name = command.Name;
        legalStatus.Siret = command.Siret;
        legalStatus.VatNumber = command.VatNumber;
        legalStatus.VatExemption = command.VatExemption;
        legalStatus.VatRate = command.VatRate;
        legalStatus.TaxDeductionExemption = command.TaxDeductionExemption;
        legalStatus.ValidFrom = command.ValidFrom;
        legalStatus.ValidTo = command.ValidTo;
        legalStatus.UpdatedAt = DateTimeOffset.UtcNow;

        await repository.UpdateAsync(legalStatus, cancellationToken);

        return OperationResult.Success(legalStatus.Id);
    }
}
