using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.Freelances.Update;

[UsedImplicitly]
public record UpdateFreelancePersonalDataRequest(
    string FirstName,
    string LastName,
    string Email,
    string? Phone);

public class UpdateFreelancePersonalDataRequestValidator : Validator<UpdateFreelancePersonalDataRequest>
{
    public UpdateFreelancePersonalDataRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Phone).MaximumLength(15);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
    }
}

public class UpdateFreelancePersonalDataEndpoint : ApiEndpoint<UpdateFreelancePersonalDataRequest, Guid>
{
    public override void Configure()
    {
        Put("/freelance/{freelanceId:guid}/personaldata");
        Roles("user");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateFreelancePersonalDataRequest request,
        CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new UpdateFreelancePersonalDataCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Phone,
            appUserId,
            freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
