namespace WordsmithHub.Domain.FreelanceAggregate;

public interface IFreelanceFactory
{
    Freelance CreateFreelanceProfile(
        Guid appUserId,
        string? firstName,
        string? lastName,
        string email
    );

    Freelance CompleteFreelanceProfile(
        Freelance freelance,
        string firstName,
        string lastName,
        string email,
        string? phone,
        Address address
    );
}

public class FreelanceFactory : IFreelanceFactory
{
    public Freelance CreateFreelanceProfile(
        Guid appUserId,
        string? firstName,
        string? lastName,
        string email)
    {
        var freelance = new Freelance
        {
            Id = Guid.NewGuid(),
            FirstName = firstName ?? string.Empty,
            LastName = lastName ?? string.Empty,
            Email = email,
            AppUserId = appUserId,
            StatusId = StatusIds.General.Draft,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return freelance;
    }

    public Freelance CompleteFreelanceProfile(
        Freelance freelance,
        string firstName,
        string lastName,
        string email,
        string? phone,
        Address address)
    {
        freelance.FirstName = firstName;
        freelance.LastName = lastName;
        freelance.Email = email;
        freelance.Phone = phone ?? string.Empty;
        freelance.Address = address;
        freelance.StatusId = StatusIds.General.Active;
        freelance.UpdatedAt = DateTimeOffset.UtcNow;

        return freelance;
    }
}
