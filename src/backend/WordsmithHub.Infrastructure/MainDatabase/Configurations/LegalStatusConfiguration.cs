using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain.LegalStatusAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class LegalStatusConfiguration : IEntityTypeConfiguration<LegalStatus>
{
    public void Configure(EntityTypeBuilder<LegalStatus> builder)
    {
        builder.ToTable("LegalStatuses");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Siret).HasMaxLength(14);
        builder.Property(s => s.VatNumber).HasMaxLength(13);
        builder.Property(s => s.VatExemption).IsRequired();
        builder.Property(s => s.VatRate).HasColumnType("decimal(4,2)");
        builder.Property(s => s.TaxDeductionExemption).IsRequired();
        builder.Property(s => s.ValidFrom).IsRequired().HasColumnType("date");
        builder.Property(s => s.ValidTo).HasColumnType("date");
        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt).IsRequired();
        builder
            .HasOne(s => s.Freelance)
            .WithMany()
            .HasForeignKey(s => s.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(s => s.Status)
            .WithMany()
            .HasForeignKey(s => s.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}