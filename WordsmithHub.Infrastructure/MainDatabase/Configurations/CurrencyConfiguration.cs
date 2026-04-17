using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(3);
        builder.Property(c => c.Symbol).IsRequired().HasMaxLength(3);
        // Seed data
        builder.HasData(
            new Currency { Id = 1, Name = "Dollar", Code = "USD", Symbol = "$" },
            new Currency { Id = 2, Name = "Euro", Code = "EUR", Symbol = "€" }
        );
    }
}
