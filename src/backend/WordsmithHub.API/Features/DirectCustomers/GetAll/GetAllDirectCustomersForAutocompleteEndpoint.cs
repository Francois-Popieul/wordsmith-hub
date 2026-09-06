using FastEndpoints;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;

namespace WordsmithHub.API.Features.DirectCustomers.GetAll;

[UsedImplicitly]
public record GetAllDirectCustomersForAutocompleteRequest(
    string UserInput);

public class
    GetAllDirectCustomersForAutocompleteEndpoint : ApiEndpoint<GetAllDirectCustomersForAutocompleteRequest,
    IReadOnlyList<string>>
{
    public override void Configure()
    {
        Get("/directcustomerlist");
        Roles("user", "admin");
        Description(x => x.WithTags("directcustomer")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(GetAllDirectCustomersForAutocompleteRequest request,
        CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new GetAllDirectCustomersForAutocompleteCommand(request.UserInput, appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
