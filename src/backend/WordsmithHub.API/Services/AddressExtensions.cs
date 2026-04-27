using WordsmithHub.API.Features.Common.Models;
using WordsmithHub.Domain;

namespace WordsmithHub.API.Services;

public static class AddressExtensions
{
    public static AddressDto ToDto(this Address address)
    {
        ArgumentNullException.ThrowIfNull(address);

        return new AddressDto
        {
            StreetInfo = address.StreetInfo,
            AddressComplement = address.AddressComplement,
            City = address.City,
            PostCode = address.PostCode,
            State = address.State,
            CountryId = address.CountryId
        };
    }
}
