using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class TranslationLanguageConfiguration : IEntityTypeConfiguration<TranslationLanguage>
{
    public void Configure(EntityTypeBuilder<TranslationLanguage> builder)
    {
        builder.ToTable("TranslationLanguages");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).IsRequired().ValueGeneratedNever();
        builder.Property(l => l.Name).IsRequired().HasMaxLength(25);
        builder.Property(l => l.Code).IsRequired().HasMaxLength(2);
        // Seed data
        builder.HasData(
            new TranslationLanguage { Id = 1, Name = "Anglais", Code = "EN" },
            new TranslationLanguage { Id = 2, Name = "Français", Code = "FR" },
            new TranslationLanguage { Id = 3, Name = "Espagnol", Code = "ES" },
            new TranslationLanguage { Id = 4, Name = "Allemand", Code = "DE" },
            new TranslationLanguage { Id = 5, Name = "Italien", Code = "IT" },
            new TranslationLanguage { Id = 6, Name = "Portugais", Code = "PT" },
            new TranslationLanguage { Id = 7, Name = "Néerlandais", Code = "NL" },
            new TranslationLanguage { Id = 8, Name = "Russe", Code = "RU" },
            new TranslationLanguage { Id = 9, Name = "Japonais", Code = "JA" },
            new TranslationLanguage { Id = 10, Name = "Chinois", Code = "ZH" },
            new TranslationLanguage { Id = 11, Name = "Arabe", Code = "AR" },
            new TranslationLanguage { Id = 12, Name = "Hindi", Code = "HI" },
            new TranslationLanguage { Id = 13, Name = "Coréen", Code = "KO" },
            new TranslationLanguage { Id = 14, Name = "Turc", Code = "TR" },
            new TranslationLanguage { Id = 15, Name = "Polonais", Code = "PL" },
            new TranslationLanguage { Id = 16, Name = "Hébreu", Code = "HE" },
            new TranslationLanguage { Id = 17, Name = "Urdu", Code = "UR" },
            new TranslationLanguage { Id = 18, Name = "Vietnamien", Code = "VI" },
            new TranslationLanguage { Id = 19, Name = "Suédois", Code = "SV" },
            new TranslationLanguage { Id = 20, Name = "Danois", Code = "DA" },
            new TranslationLanguage { Id = 21, Name = "Finnois", Code = "FI" },
            new TranslationLanguage { Id = 22, Name = "Norvégien", Code = "NO" },
            new TranslationLanguage { Id = 23, Name = "Grec", Code = "EL" },
            new TranslationLanguage { Id = 24, Name = "Tchèque", Code = "CS" },
            new TranslationLanguage { Id = 25, Name = "Slovaque", Code = "SK" },
            new TranslationLanguage { Id = 26, Name = "Hongrois", Code = "HU" },
            new TranslationLanguage { Id = 27, Name = "Roumain", Code = "RO" },
            new TranslationLanguage { Id = 28, Name = "Bulgare", Code = "BG" },
            new TranslationLanguage { Id = 29, Name = "Ukrainien", Code = "UK" },
            new TranslationLanguage { Id = 30, Name = "Serbe", Code = "SR" },
            new TranslationLanguage { Id = 31, Name = "Croate", Code = "HR" },
            new TranslationLanguage { Id = 32, Name = "Slovène", Code = "SL" },
            new TranslationLanguage { Id = 33, Name = "Indonésien", Code = "ID" },
            new TranslationLanguage { Id = 34, Name = "Malais", Code = "MS" },
            new TranslationLanguage { Id = 35, Name = "Thaï", Code = "TH" },
            new TranslationLanguage { Id = 36, Name = "Bengali", Code = "BN" },
            new TranslationLanguage { Id = 37, Name = "Tamoul", Code = "TA" },
            new TranslationLanguage { Id = 38, Name = "Persan (Farsi)", Code = "FA" }
        );
    }
}
