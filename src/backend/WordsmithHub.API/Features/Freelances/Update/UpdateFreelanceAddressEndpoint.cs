using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.Freelances.Update;

[UsedImplicitly]
public record UpdateFreelanceAddressRequest(Address? Address);

public class UpdateFreelanceAddressRequestValidator : Validator<UpdateFreelanceAddressRequest>
{
    public UpdateFreelanceAddressRequestValidator()
    {
        RuleFor(x => x.Address)
            .NotNull().WithMessage("L'adresse est requise.");
        When(x => x.Address != null, () =>
        {
            RuleFor(x => x.Address!.StreetInfo)
                .MaximumLength(255)
                .WithMessage("Le numéro et le nom de rue ne doivent pas faire plus de 255 caractères.");
            RuleFor(x => x.Address!.AddressComplement)
                .MaximumLength(255).WithMessage("Le complément d’adresse ne doit pas faire plus de 255 caractères.");
            RuleFor(x => x.Address!.PostCode)
                .MaximumLength(10).WithMessage("Le code postal ne doit pas faire plus de 10 caractères.");
            RuleFor(x => x.Address!.State)
                .MaximumLength(50)
                .WithMessage("Le nom de l’État ou de la région ne doit pas faire plus de 50 caractères.");
            RuleFor(x => x.Address!.City)
                .MaximumLength(100).WithMessage("Le nom de la ville ne doit pas faire plus de 100 caractères.");
        });
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
            request.Address!,
            appUserId,
            freelanceId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
