using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Rates.Add;

namespace WordsmithHub.API.Features.Rates.Update;

[UsedImplicitly]
public record UpdateRateRequest(
    decimal UnitPrice,
    string Unit,
    int SourceLanguageId,
    int TargetLanguageId,
    int ServiceId,
    Guid DirectCustomerId);

public class UpdateRateRequestValidator : Validator<UpdateRateRequest>
{
    public UpdateRateRequestValidator()
    {
        RuleFor(x => x.DirectCustomerId.ToString())
            .NotEmpty()
            .MaximumLength(36);
        RuleFor(x => x.ServiceId)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.SourceLanguageId)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.TargetLanguageId)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.UnitPrice)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Unit)
            .NotEmpty()
            .MaximumLength(20);
    }
}

public class UpdateRateEndpoint : ApiEndpoint<UpdateRateRequest, Guid>
{
    public override void Configure()
    {
        Put("/rate/{rateId:guid}");
        Roles("user");
        Description(x => x.WithTags("rate")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateRateRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var rateId = Route<Guid>("rateId");

        var command = new UpdateRateCommand(
            rateId,
            request.UnitPrice,
            request.Unit,
            request.SourceLanguageId,
            request.TargetLanguageId,
            request.ServiceId,
            request.DirectCustomerId,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
