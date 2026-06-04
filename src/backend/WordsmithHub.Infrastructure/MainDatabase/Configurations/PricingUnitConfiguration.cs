using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class PricingUnitConfiguration : IEntityTypeConfiguration<PricingUnit>
{
    public void Configure(EntityTypeBuilder<PricingUnit> builder)
    {
        builder.ToTable("PricingUnits");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
        // Seed data
        builder.HasData(
            new PricingUnit { Id = 1, Name = "par mot", Code = "perWord" },
            new PricingUnit { Id = 2, Name = "par minute", Code = "perMinute" },
            new PricingUnit { Id = 3, Name = "par heure", Code = "perHour" },
            new PricingUnit { Id = 4, Name = "par jour", Code = "perDay" },
            new PricingUnit { Id = 5, Name = "forfait", Code = "flatRate" }
        );
    }
}
