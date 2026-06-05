using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.LegalStatuses.Add;

[UsedImplicitly]
public record AddLegalStatusRequest(
    int LegalStatusTypeId,
    string? Siret,
    string? VatNumber,
    bool VatExemption,
    decimal? VatRate,
    bool TaxDeductionExemption,
    DateTimeOffset ValidFrom,
    DateTimeOffset? ValidTo);

public class AddLegalStatusRequestValidator : Validator<AddLegalStatusRequest>
{
    public AddLegalStatusRequestValidator()
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

public class AddLegalStatusEndpoint : ApiEndpoint<AddLegalStatusRequest, Guid>
{
    public override void Configure()
    {
        Post("/legalstatus");
        Roles("user");
        Description(x => x.WithTags("legalstatus")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(AddLegalStatusRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new AddLegalStatusCommand(
            request.LegalStatusTypeId,
            request.Siret ?? null,
            request.VatNumber ?? null,
            request.VatExemption,
            request.VatRate ?? null,
            request.TaxDeductionExemption,
            request.ValidFrom,
            request.ValidTo ?? null,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
