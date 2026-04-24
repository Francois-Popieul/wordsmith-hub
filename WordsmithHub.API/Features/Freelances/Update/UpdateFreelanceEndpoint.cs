using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Freelances.Update;

[UsedImplicitly]
public record UpdateFreelanceRequest(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    Address Address);

public class UpdateFreelanceRequestValidator : Validator<UpdateFreelanceRequest>
{
    public UpdateFreelanceRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Phone).MaximumLength(15);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.Address).NotNull();
    }
}

public class UpdateFreelanceEndpoint : Endpoint<UpdateFreelanceRequest>
{
    public override void Configure()
    {
        Put("/freelance/{freelanceId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateFreelanceRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new UpdateFreelanceCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Phone,
            request.Address,
            appUserId,
            freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        switch (result.Status)
        {
            case OperationStatus.Forbidden:
                await Send.ForbiddenAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value, cancellationToken);
                return;
        }
    }
}
