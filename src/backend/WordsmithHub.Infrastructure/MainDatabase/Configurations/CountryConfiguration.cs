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
            new Country { Id = 13, Code = "JPN", Name = "Japon", IsEuropeanUnionMember = false },
            new Country { Id = 14, Code = "AUT", Name = "Autriche", IsEuropeanUnionMember = true },
            new Country { Id = 15, Code = "BGR", Name = "Bulgarie", IsEuropeanUnionMember = true },
            new Country { Id = 16, Code = "HRV", Name = "Croatie", IsEuropeanUnionMember = true },
            new Country { Id = 17, Code = "CYP", Name = "Chypre", IsEuropeanUnionMember = true },
            new Country { Id = 18, Code = "CZE", Name = "Tchéquie", IsEuropeanUnionMember = true },
            new Country { Id = 19, Code = "DNK", Name = "Danemark", IsEuropeanUnionMember = true },
            new Country { Id = 20, Code = "EST", Name = "Estonie", IsEuropeanUnionMember = true },
            new Country { Id = 21, Code = "FIN", Name = "Finlande", IsEuropeanUnionMember = true },
            new Country { Id = 22, Code = "GRC", Name = "Grèce", IsEuropeanUnionMember = true },
            new Country { Id = 23, Code = "HUN", Name = "Hongrie", IsEuropeanUnionMember = true },
            new Country { Id = 24, Code = "IRL", Name = "Irlande", IsEuropeanUnionMember = true },
            new Country { Id = 25, Code = "LVA", Name = "Lettonie", IsEuropeanUnionMember = true },
            new Country { Id = 26, Code = "LTU", Name = "Lituanie", IsEuropeanUnionMember = true },
            new Country { Id = 27, Code = "LUX", Name = "Luxembourg", IsEuropeanUnionMember = true },
            new Country { Id = 28, Code = "MLT", Name = "Malte", IsEuropeanUnionMember = true },
            new Country { Id = 29, Code = "POL", Name = "Pologne", IsEuropeanUnionMember = true },
            new Country { Id = 30, Code = "ROU", Name = "Roumanie", IsEuropeanUnionMember = true },
            new Country { Id = 31, Code = "SVK", Name = "Slovaquie", IsEuropeanUnionMember = true },
            new Country { Id = 32, Code = "SVN", Name = "Slovénie", IsEuropeanUnionMember = true },
            new Country { Id = 33, Code = "SWE", Name = "Suède", IsEuropeanUnionMember = true },
            new Country { Id = 34, Code = "BRA", Name = "Brésil", IsEuropeanUnionMember = false },
            new Country { Id = 35, Code = "MEX", Name = "Mexique", IsEuropeanUnionMember = false },
            new Country { Id = 36, Code = "ARG", Name = "Argentine", IsEuropeanUnionMember = false },
            new Country { Id = 37, Code = "CHL", Name = "Chili", IsEuropeanUnionMember = false },
            new Country { Id = 38, Code = "CHN", Name = "Chine", IsEuropeanUnionMember = false },
            new Country { Id = 39, Code = "KOR", Name = "Corée du Sud", IsEuropeanUnionMember = false },
            new Country { Id = 40, Code = "IND", Name = "Inde", IsEuropeanUnionMember = false },
            new Country { Id = 41, Code = "IDN", Name = "Indonésie", IsEuropeanUnionMember = false },
            new Country { Id = 42, Code = "RUS", Name = "Russie", IsEuropeanUnionMember = false },
            new Country { Id = 43, Code = "TUR", Name = "Turquie", IsEuropeanUnionMember = false },
            new Country { Id = 44, Code = "SAU", Name = "Arabie saoudite", IsEuropeanUnionMember = false },
            new Country { Id = 45, Code = "ZAF", Name = "Afrique du Sud", IsEuropeanUnionMember = false },
            new Country { Id = 46, Code = "NOR", Name = "Norvège", IsEuropeanUnionMember = false },
            new Country { Id = 47, Code = "ISL", Name = "Islande", IsEuropeanUnionMember = false },
            new Country { Id = 48, Code = "UKR", Name = "Ukraine", IsEuropeanUnionMember = false },
            new Country { Id = 49, Code = "SRB", Name = "Serbie", IsEuropeanUnionMember = false },
            new Country { Id = 50, Code = "MAR", Name = "Maroc", IsEuropeanUnionMember = false },
            new Country { Id = 51, Code = "TUN", Name = "Tunisie", IsEuropeanUnionMember = false },
            new Country { Id = 52, Code = "EGY", Name = "Égypte", IsEuropeanUnionMember = false },
            new Country { Id = 53, Code = "DZA", Name = "Algérie", IsEuropeanUnionMember = false }
        );
    }
}
