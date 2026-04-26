using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WordsmithHub.API.Tests;

public class TestAuthSchemeOptions : AuthenticationSchemeOptions
{
    public string? Roles { get; set; }
    public Guid AppUserId { get; set; }
}

public class TestAuthHandler(
    IOptionsMonitor<TestAuthSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<TestAuthSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity("Test");
        identity.AddClaim(new Claim("sub", Options.AppUserId.ToString()));

        if (!string.IsNullOrWhiteSpace(Options.Roles))
        {
            foreach (var role in Options.Roles.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Trim().ToLowerInvariant()));
            }
        }

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
