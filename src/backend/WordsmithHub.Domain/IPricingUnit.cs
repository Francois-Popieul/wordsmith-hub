namespace WordsmithHub.Domain;

public interface IPricingUnit
{
    Task<IReadOnlyList<PricingUnit>> GetAllAsync(CancellationToken cancellationToken = default);
}
