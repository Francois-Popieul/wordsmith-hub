using WordsmithHub.API.Features.Freelances.Models;
using WordsmithHub.API.Services;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.API.Features.Freelances.Services;

public static class FreelanceExtensions
{
    public static FreelanceDto ToDto(this Freelance freelance)
    {
        ArgumentNullException.ThrowIfNull(freelance);

        if (freelance.Address != null)
        {
            return new FreelanceDto
            {
                Id = freelance.Id,
                FirstName = freelance.FirstName ?? string.Empty,
                LastName = freelance.LastName ?? string.Empty,
                Email = freelance.Email,
                Phone = freelance.Phone ?? string.Empty,
                Address = freelance.Address.ToDto(),
                StatusId = freelance.StatusId
            };
        }

        return new FreelanceDto
        {
            Id = freelance.Id,
            FirstName = freelance.FirstName ?? string.Empty,
            LastName = freelance.LastName ?? string.Empty,
            Email = freelance.Email,
            Address = null,
            Phone = freelance.Phone ?? string.Empty,
            StatusId = freelance.StatusId
        };
    }
}
