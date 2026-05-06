using FastEndpoints;
using FluentValidation;
using JetBrains.Annotations;
using WordsmithHub.API.Features.Common;
using WordsmithHub.API.Features.Common.AppUserIdPreprocessing;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Features.DirectCustomers.Add;

[UsedImplicitly]
public record AddDirectCustomerRequest(
    string Name,
    string Code,
    string? Phone,
    string Email,
    Address Address,
    string? SiretOrSiren,
    int PaymentDelay,
    int CurrencyId);

public class AddDirectCustomerRequestValidator : Validator<AddDirectCustomerRequest>
{
    public AddDirectCustomerRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Phone).MaximumLength(15);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.Address.StreetInfo).MaximumLength(255);
        RuleFor(x => x.Address.AddressComplement).MaximumLength(255);
        RuleFor(x => x.Address.PostCode).MaximumLength(10);
        RuleFor(x => x.Address.State).MaximumLength(50);
        RuleFor(x => x.Address.City).MaximumLength(100);
        RuleFor(x => x.SiretOrSiren).MaximumLength(15);
        RuleFor(x => x.PaymentDelay).NotEmpty();
        RuleFor(x => x.CurrencyId).NotEmpty();
    }
}

public class AddDirectCustomerEndpoint : ApiEndpoint<AddDirectCustomerRequest, Guid>
{
    public override void Configure()
    {
        Post("/directcustomer");
        Roles("user", "admin");
        Description(x => x.WithTags("directcustomer")
            .Produces(StatusCodes.Status403Forbidden));
    }

    public override async Task HandleAsync(AddDirectCustomerRequest request, CancellationToken cancellationToken)
    {
        var appUserId = (Guid)HttpContext.Items[HttpContextItemKeys.AppUserId]!;

        var command = new AddDirectCustomerCommand(
            request.Name,
            request.Code,
            request.Phone,
            request.Email,
            request.Address,
            request.SiretOrSiren,
            request.PaymentDelay,
            request.CurrencyId,
            appUserId);

        var result = await command.ExecuteAsync(cancellationToken);

        await SendResult(result, cancellationToken);
    }
}
