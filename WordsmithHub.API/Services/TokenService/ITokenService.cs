using WordsmithHub.Infrastructure.IdentityDatabase;

namespace WordsmithHub.API.Services.TokenService;

public interface ITokenService
{
    Task<string> CreateAccessTokenAsync(AppUser user);
}