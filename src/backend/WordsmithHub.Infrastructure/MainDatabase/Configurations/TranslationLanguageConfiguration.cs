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
            new TranslationLanguage { Id = 15, Name = "Polonais", Code = "PL" }
        );
    }
}
