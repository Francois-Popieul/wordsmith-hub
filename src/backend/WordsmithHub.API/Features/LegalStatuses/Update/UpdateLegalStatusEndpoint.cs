using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.LegalStatuses.Update;

[UsedImplicitly]
public record UpdateLegalStatusRequest(
    int LegalStatusTypeId,
    string? Siret,
    string? VatNumber,
    bool VatExemption,
    decimal? VatRate,
    bool TaxDeductionExemption,
    DateTimeOffset ValidFrom,
    DateTimeOffset? ValidTo);

public class UpdateLegalStatusRequestValidator : Validator<UpdateLegalStatusRequest>
{
    public UpdateLegalStatusRequestValidator()
    {
        RuleFor(x => x.LegalStatusTypeId)
            .NotEmpty().WithMessage("Le type de statut juridique est requis.");
        RuleFor(x => x.Siret).MaximumLength(14).WithMessage("Le numéro d'immatriculation est limité à 14 caractères.");
        RuleFor(x => x.VatNumber)
            .MaximumLength(13).WithMessage("Le numéro de TVA est limité à 14 caractères.");
        RuleFor(x => x.VatRate)
            .GreaterThanOrEqualTo(0).WithMessage("Le taux de TVA doit être supérieur ou égal à 0.");
        RuleFor(x => x.ValidFrom)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow)
            .WithMessage("La date dé début de validité doit être antérieure à la date du jour.");
        RuleFor(x => x.ValidTo)
            .GreaterThanOrEqualTo(x => x.ValidFrom)
            .WithMessage("La date dé fint de validité doit être postérieure à la date de début de validité.");
    }
}

public class UpdateLegalStatusEndpoint : ApiEndpoint<UpdateLegalStatusRequest, Guid>
{
    public override void Configure()
    {
        Put("/legalstatus/{legalStatusId:guid}");
        Roles("user");
        Description(x => x.WithTags("legalstatus")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateLegalStatusRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var legalStatusId = Route<Guid>("legalStatusId");

        var command = new UpdateLegalStatusCommand(
            appUserId,
            legalStatusId,
            request.LegalStatusTypeId,
            request.Siret ?? null,
            request.VatNumber ?? null,
            request.VatExemption,
            request.VatRate ?? null,
            request.TaxDeductionExemption,
            request.ValidFrom,
            request.ValidTo ?? null);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
