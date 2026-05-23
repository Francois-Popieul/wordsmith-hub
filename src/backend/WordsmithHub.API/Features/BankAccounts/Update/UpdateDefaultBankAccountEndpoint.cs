using FastEndpoints;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.BankAccounts.Update;

public class UpdateDefaultBankAccountEndpoint : ApiEndpoint<Guid, Guid>
{
    public override void Configure()
    {
        Put("/bankaccount/{bankAccountId:guid}");
        Roles("user");
        Description(x => x.WithTags("bankaccount")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(Guid request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var bankAccountId = Route<Guid>("bankAccountId");

        var command = new UpdateDefaultBankAccountCommand(appUserId, bankAccountId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
