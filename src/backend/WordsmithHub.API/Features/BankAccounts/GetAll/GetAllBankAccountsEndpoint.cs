using FastEndpoints;
using WordsmithHub.API.Features.BankAccounts.Models;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.BankAccounts.GetAll;

public class GetAllBankAccountsEndpoint : ApiEndpointWithoutRequest<IReadOnlyList<BankAccountDto>>
{
    public override void Configure()
    {
        Get("/bankaccounts");
        Roles("user", "admin");
        Description(x => x.WithTags("bankaccount")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllBankAccountsCommand(appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
