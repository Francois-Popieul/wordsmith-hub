using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).IsRequired().ValueGeneratedNever();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(30);
        // Seed data
        builder.HasData(
            new Service { Id = 1, Name = "Traduction" },
            new Service { Id = 2, Name = "Relecture" },
            new Service { Id = 3, Name = "Sous-titrage" },
            new Service { Id = 4, Name = "Post-édition" },
            new Service { Id = 5, Name = "Contrôle qualité" },
            new Service { Id = 6, Name = "Transcription" },
            new Service { Id = 7, Name = "Traduction certifiée" },
            new Service { Id = 8, Name = "Localisation" },
            new Service { Id = 9, Name = "Transcréation" },
            new Service { Id = 10, Name = "Révision bilingue" },
            new Service { Id = 11, Name = "Correction monolingue" },
            new Service { Id = 12, Name = "Alignement de documents" },
            new Service { Id = 13, Name = "Gestion terminologique" },
            new Service { Id = 14, Name = "Création de glossaire" },
            new Service { Id = 15, Name = "Traduction SEO" },
            new Service { Id = 16, Name = "Voix off" },
            new Service { Id = 17, Name = "Doublage" },
            new Service { Id = 18, Name = "Interprétation simultanée" },
            new Service { Id = 19, Name = "Interprétation consécutive" },
            new Service { Id = 20, Name = "Interprétation téléphonique" },
            new Service { Id = 21, Name = "Mise en page (DTP)" },
            new Service { Id = 22, Name = "Formatage de fichiers" },
            new Service { Id = 23, Name = "Extraction de texte" },
            new Service { Id = 24, Name = "Nettoyage de fichiers" },
            new Service { Id = 25, Name = "Évaluation linguistique" }
        );
    }
}
