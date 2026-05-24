using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.BankAccounts.Add;

[UsedImplicitly]
public record AddBankAccountRequest(
    string Label,
    string BankName,
    string AccountHolderName,
    string Iban,
    string Bic);

public class AddBankAccountRequestValidator : Validator<AddBankAccountRequest>
{
    public AddBankAccountRequestValidator()
    {
        RuleFor(a => a.Label).NotNull();
        RuleFor(a => a.BankName).NotNull();
        RuleFor(a => a.AccountHolderName).NotNull();
        RuleFor(a => a.Iban).NotNull();
        RuleFor(a => a.Bic).NotNull();
    }
}

public class AddBankAccountEndpoint : ApiEndpoint<AddBankAccountRequest, Guid>
{
    public override void Configure()
    {
        Post("/bankaccount");
        Roles("user");
        Description(x => x.WithTags("bankaccount")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(AddBankAccountRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new AddBankAccountCommand(
            request.Label,
            request.BankName,
            request.AccountHolderName,
            request.Iban,
            request.Bic,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
