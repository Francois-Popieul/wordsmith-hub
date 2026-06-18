using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.Freelances.Update;

[UsedImplicitly]
public record UpdateFreelanceLanguagesRequest(
    IReadOnlyList<int> SourceLanguageIds,
    IReadOnlyList<int> TargetLanguageIds);

public class UpdateFreelanceLanguagesRequestValidator : Validator<UpdateFreelanceLanguagesRequest>
{
    public UpdateFreelanceLanguagesRequestValidator()
    {
        RuleFor(x => x.SourceLanguageIds)
            .NotEmpty().WithMessage("Veuillez renseigner au moins une langue source.")
            .Must(ids => ids.Count is > 0 and <= 38);
        RuleFor(x => x.TargetLanguageIds)
            .NotEmpty().WithMessage("Veuillez renseigner au moins une langue cible.")
            .Must(ids => ids.Count is > 0 and <= 38);
    }
}

public class UpdateFreelanceLanguagesEndpoint : ApiEndpoint<UpdateFreelanceLanguagesRequest, Guid>
{
    public override void Configure()
    {
        Put("/freelance/{freelanceId:guid}/languages");
        Roles("user");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateFreelanceLanguagesRequest request,
        CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new UpdateFreelanceLanguagesCommand(
            request.SourceLanguageIds,
            request.TargetLanguageIds,
            appUserId,
            freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
