using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WordsmithHub.Infrastructure.IdentityDatabase;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public new virtual DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IdentityDbContext).Assembly,
            type => type.Namespace != null &&
                    type.Namespace.StartsWith(
                        "WordsmithHub.Infrastructure.IdentityDatabase.Configurations",
                        StringComparison.Ordinal));

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = "1" },
            new IdentityRole { Id = "2", Name = "user", NormalizedName = "USER", ConcurrencyStamp = "2" }
        );

        const string adminId = "8feb56a9-5b14-4a47-be5f-b56e1c822e1c";

        var user = new AppUser
        {
            Id = adminId,
            FirstName = "François",
            LastName = "Popieul",
            UserName = "admin@wordsmithhub.com",
            NormalizedUserName = "ADMIN@WORDSMITHHUB.COM",
            Email = "admin@wordsmithhub.com",
            NormalizedEmail = "ADMIN@WORDSMITHHUB.COM",
            EmailConfirmed = true,
            SecurityStamp = "9707faae-95d6-46a7-8123-0ff0bbdbc75a",
            ConcurrencyStamp = "76b12994-2ff3-48d7-a22d-04ca77d040b7",
            PasswordHash = "AQAAAAIAAYagAAAAEAlbrepvlKVebZAroVmr5FbaT7Vrw7NJ4xlz+tywUvL5PcTuiA2S0Bp2cduePDp9Eg=="
        };

        modelBuilder.Entity<AppUser>().HasData(user);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminId, RoleId = "1" }
        );
    }
}
