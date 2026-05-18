using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.LegalStatuses.Add;

[UsedImplicitly]
public record AddLegalStatusRequest(
    string Name,
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
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Siret).MaximumLength(14);
        RuleFor(x => x.VatNumber).MaximumLength(13);
        RuleFor(x => x.VatRate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ValidFrom).LessThanOrEqualTo(DateTimeOffset.UtcNow);
        RuleFor(x => x.ValidTo).GreaterThanOrEqualTo(x => x.ValidFrom);
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
            request.Name,
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
