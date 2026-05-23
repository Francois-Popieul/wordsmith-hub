using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.LegalStatuses.Update;

[UsedImplicitly]
public record UpdateLegalStatusRequest(
    Guid LegalStatusId,
    string Name,
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
        RuleFor(x => x.LegalStatusId).NotEmpty();
    }
}

public class UpdateLegalStatusEndpoint : ApiEndpoint<UpdateLegalStatusRequest, Guid>
{
    public override void Configure()
    {
        Put("/legalstatus");
        Roles("user");
        Description(x => x.WithTags("legalstatus")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateLegalStatusRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new UpdateLegalStatusCommand(
            appUserId,
            request.LegalStatusId,
            request.Name,
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
