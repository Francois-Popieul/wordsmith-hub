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
        RuleFor(x => x.Label).NotEmpty().MaximumLength(100);
        RuleFor(x => x.BankName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.AccountHolderName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Iban).NotEmpty().MaximumLength(34);
        RuleFor(x => x.Bic).NotEmpty().MaximumLength(11);
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
