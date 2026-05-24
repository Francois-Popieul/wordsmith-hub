using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.BankAccounts.Update;

[UsedImplicitly]
public record UpdateBankAccountRequest(
    Guid BankAccountId,
    string Label,
    string BankName,
    string AccountHolderName,
    string Iban,
    string Bic,
    bool IsDefault);

public class UpdateBankAccountRequestValidator : Validator<UpdateBankAccountRequest>
{
    public UpdateBankAccountRequestValidator()
    {
        RuleFor(x => x.BankAccountId).NotEmpty();
        RuleFor(x => x.Label).NotEmpty().MaximumLength(100);
        RuleFor(x => x.BankName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.AccountHolderName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Iban).NotEmpty().MaximumLength(34);
        RuleFor(x => x.Bic).NotEmpty().MaximumLength(11);
        RuleFor(x => x.IsDefault).NotNull();
    }
}

public class UpdateBankAccountEndpoint : ApiEndpoint<UpdateBankAccountRequest, Guid>
{
    public override void Configure()
    {
        Put("/bankaccount");
        Roles("user");
        Description(x => x.WithTags("bankaccount")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(UpdateBankAccountRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new UpdateBankAccountCommand(
            appUserId,
            request.BankAccountId,
            request.Label,
            request.BankName,
            request.AccountHolderName,
            request.Iban,
            request.Bic,
            request.IsDefault);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
