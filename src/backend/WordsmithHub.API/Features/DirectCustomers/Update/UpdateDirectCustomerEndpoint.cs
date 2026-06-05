using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.DirectCustomers.Update;

[UsedImplicitly]
public record UpdateDirectCustomerRequest(
    string Name,
    string Code,
    string? Phone,
    string Email,
    Address? Address,
    string? SiretOrSiren,
    int PaymentDelay,
    int CurrencyId);

public class UpdateDirectCustomerRequestValidator : Validator<UpdateDirectCustomerRequest>
{
    public UpdateDirectCustomerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Le nom du client est requis.")
            .MaximumLength(150).WithMessage("Le nom du client ne doit pas faire plus de 150 caractères.");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Le code du client est requis.")
            .MaximumLength(5).WithMessage("Le code du client ne doit pas faire plus de 5 caractères.");
        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Le numéro de téléphone ne doit pas faire plus de 20 caractères.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L’adresse e-mail du client est requise.")
            .EmailAddress().WithMessage("Veuillez saisir une adresse e-mail valide.")
            .MaximumLength(255).WithMessage("L'adresse e-mail ne doit pas faire plus de 255 caractères.");
        RuleFor(x => x.Address)
            .NotNull().WithMessage("L'adresse du client est requise.");
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
        RuleFor(x => x.SiretOrSiren)
            .MaximumLength(15).WithMessage("Le numéro d’immatriculation ne doit pas faire plus de 15 caractères.");
        RuleFor(x => x.PaymentDelay)
            .NotEmpty().WithMessage("Le délai de paiement est requis.");
        RuleFor(x => x.CurrencyId)
            .NotEmpty().WithMessage("La devise est requise.");
    }
}

public class UpdateDirectCustomerEndpoint : ApiEndpoint<UpdateDirectCustomerRequest, Guid>
{
    public override void Configure()
    {
        Put("/directcustomer/{directCustomerId:guid}");
        Roles("user", "admin");
        Description(x => x.WithTags("directcustomer")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateDirectCustomerRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var directCustomerId = Route<Guid>("directCustomerId");

        var command = new UpdateDirectCustomerCommand(
            request.Name,
            request.Code,
            request.Phone,
            request.Email,
            request.Address!,
            request.SiretOrSiren,
            request.PaymentDelay,
            request.CurrencyId,
            appUserId,
            directCustomerId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
