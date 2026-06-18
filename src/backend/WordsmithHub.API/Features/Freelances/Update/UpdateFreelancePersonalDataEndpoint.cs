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
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Le prénom est requis.")
            .MaximumLength(50).WithMessage("Le prénom est limité à 50 caractères.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Le nom est requis.")
            .MaximumLength(100).WithMessage("Le nom est limité à 100 caractères.");
        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Le numéro de téléphone est limité à 20 caractères.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L’adresse e-mail est requise.")
            .EmailAddress().WithMessage("Veuillez saisir une adresse e-mail valide.")
            .MaximumLength(255).WithMessage("L’adresse e-mail est limitée à 255 caractères.");
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
