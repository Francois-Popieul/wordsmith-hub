using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(3);
        builder.Property(c => c.IsEuropeanUnionMember).IsRequired();
        // Seed data
        builder.HasData(
            new Country { Id = 1, Code = "FRA", Name = "France", IsEuropeanUnionMember = true },
            new Country { Id = 2, Code = "DEU", Name = "Allemagne", IsEuropeanUnionMember = true },
            new Country { Id = 3, Code = "ESP", Name = "Espagne", IsEuropeanUnionMember = true },
            new Country { Id = 4, Code = "ITA", Name = "Italie", IsEuropeanUnionMember = true },
            new Country { Id = 5, Code = "PRT", Name = "Portugal", IsEuropeanUnionMember = true },
            new Country { Id = 6, Code = "BEL", Name = "Belgique", IsEuropeanUnionMember = true },
            new Country { Id = 7, Code = "NLD", Name = "Pays-Bas", IsEuropeanUnionMember = true },
            new Country { Id = 8, Code = "CHE", Name = "Suisse", IsEuropeanUnionMember = false },
            new Country { Id = 9, Code = "GBR", Name = "Royaume-Uni", IsEuropeanUnionMember = false },
            new Country { Id = 10, Code = "USA", Name = "États-Unis d’Amérique", IsEuropeanUnionMember = false },
            new Country { Id = 11, Code = "CAN", Name = "Canada", IsEuropeanUnionMember = false },
            new Country { Id = 12, Code = "AUS", Name = "Australie", IsEuropeanUnionMember = false },
            new Country { Id = 13, Code = "JPN", Name = "Japon", IsEuropeanUnionMember = false }
        );
    }
}
