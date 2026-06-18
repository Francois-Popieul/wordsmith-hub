using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.Rates.Add;

[UsedImplicitly]
public record AddRateRequest(
    decimal UnitPrice,
    string Unit,
    int SourceLanguageId,
    int TargetLanguageId,
    int ServiceId,
    Guid DirectCustomerId
);

public class AddRateRequestValidator : Validator<AddRateRequest>
{
    public AddRateRequestValidator()
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

public class AddRateEndpoint : ApiEndpoint<AddRateRequest, Guid>
{
    public override void Configure()
    {
        Post("/rate");
        Roles("user");
        Description(x => x.WithTags("rate")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(AddRateRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new AddRateCommand(
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
