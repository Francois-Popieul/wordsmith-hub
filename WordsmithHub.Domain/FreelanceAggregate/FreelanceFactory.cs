namespace WordsmithHub.Domain.FreelanceAggregate;

public interface IFreelanceFactory
{
    Freelance CreateFreelance(
        Guid appUserId,
        string firstName,
        string lastName,
        string email,
        string? phone,
        Address address
    );
}

public class FreelanceFactory : IFreelanceFactory
{
    public Freelance CreateFreelance(
        Guid appUserId,
        string firstName,
        string lastName,
        string email,
        string? phone,
        Address address)
    {
        var freelance = new Freelance
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone ?? string.Empty,
            Address = address,
            AppUserId = appUserId,
            StatusId = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return freelance;
    }
}
