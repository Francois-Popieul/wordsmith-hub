using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain.OrderLineAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("OrderLines");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).ValueGeneratedNever();
        builder.Property(l => l.Quantity).HasColumnType("decimal(10,4)");
        builder.Property(l => l.AppliedUnitPrice).HasColumnType("decimal(10,4)");
        builder.Property(l => l.UsedUnit).IsRequired().HasMaxLength(20);
        builder.Property(l => l.Notes).HasMaxLength(1000);
        builder.Property(l => l.CreatedAt).IsRequired();
        builder.Property(l => l.UpdatedAt).IsRequired();
        builder
            .HasOne(l => l.WorkOrder)
            .WithMany()
            .HasForeignKey(l => l.WorkOrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(l => l.SourceLanguage)
            .WithMany()
            .HasForeignKey(l => l.SourceLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(l => l.TargetLanguage)
            .WithMany()
            .HasForeignKey(l => l.TargetLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(l => l.Service)
            .WithMany()
            .HasForeignKey(l => l.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}