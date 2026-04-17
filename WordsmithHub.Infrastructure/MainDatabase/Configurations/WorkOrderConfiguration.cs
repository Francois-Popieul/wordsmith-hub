using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.InvoiceAggregate;
using WordsmithHub.Domain.ProjectAggregate;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("WorkOrders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();
        builder.Property(o => o.Reference).IsRequired().HasMaxLength(50);
        builder.Property(o => o.StartDate).IsRequired();
        builder.Property(o => o.DeliveryDate).IsRequired();
        builder.Property(o => o.Description).HasMaxLength(1000);
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.UpdatedAt).IsRequired();
        builder
            .HasOne<Project>()
            .WithMany()
            .HasForeignKey(o => o.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Freelance>()
            .WithMany()
            .HasForeignKey(o => o.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<DirectCustomer>()
            .WithMany()
            .HasForeignKey(o => o.DirectCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Invoice>()
            .WithMany()
            .HasForeignKey(o => o.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Status>()
            .WithMany()
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
