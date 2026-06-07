using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.BankAccounts.Update;

[UsedImplicitly]
public record UpdateBankAccountRequest(
    string Label,
    string BankName,
    string AccountHolderName,
    string Iban,
    string Bic);

public class UpdateBankAccountRequestValidator : Validator<UpdateBankAccountRequest>
{
    public UpdateBankAccountRequestValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("L’intitulé est requis.")
            .MaximumLength(100).WithMessage("L’intitulé ne doit pas dépasser 100 caractères.");
        RuleFor(x => x.BankName)
            .NotEmpty().WithMessage("Le nom de la banque est requis.")
            .MaximumLength(100).WithMessage("Le nom de la banque ne doit pas dépasser 100 caractères.");
        RuleFor(x => x.AccountHolderName)
            .NotEmpty().WithMessage("Le nom du titulaire est requis.")
            .MaximumLength(100).WithMessage("Le nom du titulaire ne doit pas dépasser 100 caractères.");
        RuleFor(x => x.Iban)
            .NotEmpty().WithMessage("L’IBAN est requis.")
            .Length(34).WithMessage("L’IBAN doit comporter 34 caractères.");
        RuleFor(x => x.Bic)
            .NotEmpty().WithMessage("Le code BIC est requis.")
            .Length(11).WithMessage("Le code BIC doit comporter 11 caractères.");
    }
}

public class UpdateBankAccountEndpoint : ApiEndpoint<UpdateBankAccountRequest, Guid>
{
    public override void Configure()
    {
        Put("/bankaccount/{bankAccountId:guid}");
        Roles("user");
        Description(x => x.WithTags("bankaccount")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateBankAccountRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var bankAccountId = Route<Guid>("bankAccountId");

        var command = new UpdateBankAccountCommand(
            appUserId,
            bankAccountId,
            request.Label,
            request.BankName,
            request.AccountHolderName,
            request.Iban,
            request.Bic);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
