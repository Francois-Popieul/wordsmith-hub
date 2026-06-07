using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.WorkOrders.Add;

[UsedImplicitly]
public record AddWorkOrderRequest(
    string Reference,
    Guid ProjectId,
    Guid FreelanceId,
    Guid DirectCustomerId,
    DateTime StartDate,
    DateTime DeliveryDate,
    string? Description);

public class AddWorkOrderRequestValidator : Validator<AddWorkOrderRequest>
{
    public AddWorkOrderRequestValidator()
    {
        RuleFor(x => x.Reference)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.FreelanceId).NotEmpty();
        RuleFor(x => x.DirectCustomerId).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.DeliveryDate).NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}

public class AddWorkOrderEndpoint : ApiEndpoint<AddWorkOrderRequest, Guid>
{
    public override void Configure()
    {
        Post("/work-order");
        Roles("user");
        Description(x => x.WithTags("work-order")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(AddWorkOrderRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new AddWorkOrderCommand(
            request.Reference,
            request.ProjectId,
            request.FreelanceId,
            request.DirectCustomerId,
            request.StartDate,
            request.DeliveryDate,
            request.Description ?? string.Empty,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
