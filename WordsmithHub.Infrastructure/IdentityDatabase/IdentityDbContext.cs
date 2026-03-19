using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WordsmithHub.Infrastructure.IdentityDatabase;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<AppUser>(options);
