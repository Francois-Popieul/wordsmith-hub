using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class LegalStatusTypeConfiguration : IEntityTypeConfiguration<LegalStatusType>
{
    public void Configure(EntityTypeBuilder<LegalStatusType> builder)
    {
        builder.ToTable("LegalStatusTypes");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).IsRequired().ValueGeneratedNever();
        builder.Property(t => t.Name).IsRequired().HasMaxLength(25);
        builder.Property(t => t.Code).IsRequired().HasMaxLength(15);
        // Seed data
        builder.HasData(
            new LegalStatusType { Id = 1, Name = "Artiste-auteur", Code = "author" },
            new LegalStatusType { Id = 2, Name = "Auto-entrepreneur", Code = "self-employed" },
            new LegalStatusType { Id = 3, Name = "Portage salarial", Code = "wagePortage" },
            new LegalStatusType { Id = 4, Name = "SARL", Code = "llc" },
            new LegalStatusType { Id = 5, Name = "EURL", Code = "eurl" },
            new LegalStatusType { Id = 6, Name = "SASU", Code = "sasu" },
            new LegalStatusType { Id = 7, Name = "Entreprise individuelle", Code = "ei" }
        );
    }
}
