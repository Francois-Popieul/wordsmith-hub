using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.API.Features.Common.Results;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.DirectCustomers.Update;

[UsedImplicitly]
public record UpdateDirectCustomerRequest(
    string Name,
    string Code,
    string? Phone,
    string Email,
    Address Address,
    string? SiretOrSiren,
    int PaymentDelay,
    int CurrencyId);

public class UpdateDirectCustomerRequestValidator : Validator<UpdateDirectCustomerRequest>
{
    public UpdateDirectCustomerRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Phone).MaximumLength(15);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.SiretOrSiren).MaximumLength(15);
        RuleFor(x => x.PaymentDelay).NotEmpty();
        RuleFor(x => x.CurrencyId).NotEmpty();
    }
}

public class UpdateDirectCustomerEndpoint : Endpoint<UpdateDirectCustomerRequest>
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
            request.Address,
            request.SiretOrSiren,
            request.PaymentDelay,
            request.CurrencyId,
            appUserId,
            directCustomerId);

        var result = await command.ExecuteAsync(cancellationToken);

        switch (result.Status)
        {
            case OperationStatus.Forbidden:
                await Send.ForbiddenAsync(cancellationToken);
                return;

            case OperationStatus.Success:
                await Send.OkAsync(result.Value, cancellationToken);
                return;
        }
    }
}
