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
        RuleFor(x => x.LegalStatusTypeId).NotEmpty();
        RuleFor(x => x.Siret).MaximumLength(14);
        RuleFor(x => x.VatNumber).MaximumLength(13);
        RuleFor(x => x.VatRate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ValidFrom).LessThanOrEqualTo(DateTimeOffset.UtcNow);
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
