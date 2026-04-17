using Microsoft.EntityFrameworkCore;
using WordsmithHub.Domain;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.EndCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase;

public class MainDbContext(DbContextOptions options) : DbContext(options)
{
    // Aggregates
    public virtual DbSet<BankAccount> BankAccounts { get; set; }
    public virtual DbSet<DirectCustomer> DirectCustomers { get; set; }
    public virtual DbSet<EndCustomer> EndCustomers { get; set; }
    public virtual DbSet<Freelance> Freelances { get; set; }

    // Static Data
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Currency> Currencies { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<TranslationLanguage> TranslationLanguages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MainDbContext).Assembly,
            type => type.Namespace != null &&
                    type.Namespace.StartsWith(
                        "WordsmithHub.Infrastructure.MainDatabase.Configurations",
                        StringComparison.Ordinal));
    }
}
