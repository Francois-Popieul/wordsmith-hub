using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.API.Features.LegalStatuses.Add;

public record AddLegalStatusCommand(
    string Name,
    string? Siret,
    string? VatNumber,
    bool VatExemption,
    decimal? VatRate,
    bool TaxDeductionExemption,
    DateTimeOffset ValidFrom,
    DateTimeOffset? ValidTo,
    Guid AppUserId) : ICommand<OperationResult<Guid>>;

[UsedImplicitly]
public class AddLegalStatusHandler(
    IFreelanceRepository freelanceRepository,
    ILegalStatusRepository repository,
    ILegalStatusFactory factory) : ICommandHandler<AddLegalStatusCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> ExecuteAsync(AddLegalStatusCommand command,
        CancellationToken cancellationToken)
    {
        var freelance = await freelanceRepository.GetByAppUserIdAsync(command.AppUserId, cancellationToken);

        if (freelance == null)
        {
            return OperationResult.Forbidden<Guid>();
        }

        var legalStatus = factory.CreateLegalStatus(
            command.Name,
            command.Siret,
            command.VatNumber,
            command.VatExemption,
            command.VatRate,
            command.TaxDeductionExemption,
            command.ValidFrom,
            command.ValidTo,
            freelance.Id);

        await repository.AddAsync(legalStatus, cancellationToken);

        return OperationResult.Success(legalStatus.Id);
    }
}
