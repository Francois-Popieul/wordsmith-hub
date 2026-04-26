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
        builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
        // Seed data
        builder.HasData(
            new Service { Id = 1, Name = "Traduction" },
            new Service { Id = 2, Name = "Relecture" },
            new Service { Id = 3, Name = "Sous-titrage" },
            new Service { Id = 4, Name = "Post-édition" },
            new Service { Id = 5, Name = "Contrôle qualité" }
        );
    }
}
