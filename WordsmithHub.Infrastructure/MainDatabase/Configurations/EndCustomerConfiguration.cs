using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.EndCustomerAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class EndCustomerConfiguration : IEntityTypeConfiguration<EndCustomer>
{
    public void Configure(EntityTypeBuilder<EndCustomer> builder)
    {
        builder.ToTable("EndCustomers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();
        builder
            .HasOne(c => c.Status)
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
