using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.ProjectAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
        builder.Property(p => p.Domain).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.EndCustomerId).IsRequired(false);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
        builder
            .HasOne(p => p.Freelance)
            .WithMany()
            .HasForeignKey(p => p.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(p => p.Status)
            .WithMany()
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.EndCustomer)
            .WithMany()
            .HasForeignKey(p => p.EndCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(p => p.DirectCustomers)
            .WithMany(c => c.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectDirectCustomers",
                j => j.HasOne<DirectCustomer>()
                    .WithMany()
                    .HasForeignKey("DirectCustomerId")
                    .HasConstraintName("FK_ProjectDirectCustomers_DirectCustomer")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("ProjectId")
                    .HasConstraintName("FK_ProjectDirectCustomers_Project")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("DirectCustomerId", "ProjectId");
                    j.ToTable("ProjectDirectCustomers");
                });
    }
}
