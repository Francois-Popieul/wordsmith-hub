namespace WordsmithHub.Domain;

public interface IPricingUnitRepository
{
    Task<IReadOnlyList<PricingUnit>> GetAllAsync(CancellationToken cancellationToken = default);
}