using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Services;

public interface ITokenService
{
    Task<string> CreateAccessTokenAsync(AppUser user);
}
