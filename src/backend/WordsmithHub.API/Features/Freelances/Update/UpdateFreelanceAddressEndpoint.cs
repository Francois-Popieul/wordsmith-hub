using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Freelances.Update;

[UsedImplicitly]
public record UpdateFreelanceAddressRequest(Address Address);

public class UpdateFreelanceAddressRequestValidator : Validator<UpdateFreelanceAddressRequest>
{
    public UpdateFreelanceAddressRequestValidator()
    {
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.Address.StreetInfo).MaximumLength(255);
        RuleFor(x => x.Address.AddressComplement).MaximumLength(255);
        RuleFor(x => x.Address.PostCode).MaximumLength(10);
        RuleFor(x => x.Address.State).MaximumLength(50);
        RuleFor(x => x.Address.City).MaximumLength(100);
    }
}

public class UpdateFreelanceAddressEndpoint : ApiEndpoint<UpdateFreelanceAddressRequest, Guid>
{
    public override void Configure()
    {
        Put("/freelance/{freelanceId:guid}/address");
        Roles("user");
        Description(x => x.WithTags("freelance")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateFreelanceAddressRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var freelanceId = Route<Guid>("freelanceId");

        var command = new UpdateFreelanceAddressCommand(
            request.Address,
            appUserId,
            freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
