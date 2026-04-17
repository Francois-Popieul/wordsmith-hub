using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("Statuses");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).IsRequired().ValueGeneratedNever();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
        builder.Property(s => s.Category).IsRequired().HasMaxLength(20);
        // Seed data
        builder.HasData(
            // General
            new Status { Id = 1, Name = "Actif", Category = "General" },
            new Status { Id = 2, Name = "Inactif", Category = "General" },
            // Invoice
            new Status { Id = 10, Name = "Brouillon", Category = "Invoice" },
            new Status { Id = 11, Name = "Envoyée", Category = "Invoice" },
            new Status { Id = 12, Name = "Payée", Category = "Invoice" },
            // WorkOrder
            new Status { Id = 20, Name = "En attente", Category = "WorkOrder" },
            new Status { Id = 21, Name = "En cours", Category = "WorkOrder" },
            new Status { Id = 22, Name = "Terminée", Category = "WorkOrder" },
            new Status { Id = 23, Name = "Livrée", Category = "WorkOrder" }
        );
    }
}
