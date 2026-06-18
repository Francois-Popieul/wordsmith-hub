using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;

namespace WordsmithHub.API.Features.BankAccounts.Delete;

public class DeleteBankAccountEndpoint : ApiEndpointWithoutRequest<NoContent>
{
    public override void Configure()
    {
        Delete("/bankaccount/{bankAccountId:guid}");
        Roles("user");
        Description(x => x.WithTags("bankaccount")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var bankAccountId = Route<Guid>("bankAccountId");

        var command = new DeleteBankAccountCommand(appUserId, bankAccountId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
