namespace WordsmithHub.Domain.EndCustomerAggregate;

public interface IEndCustomerFactory
{
    EndCustomer CreateEndCustomer(string name);
}

public class EndCustomerFactory : IEndCustomerFactory
{
    public EndCustomer CreateEndCustomer(string name)
    {
        var endCustomer = new EndCustomer
        {
            Id = Guid.NewGuid(),
            Name = name,
            StatusId = StatusIds.General.Active,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return endCustomer;
    }
}
