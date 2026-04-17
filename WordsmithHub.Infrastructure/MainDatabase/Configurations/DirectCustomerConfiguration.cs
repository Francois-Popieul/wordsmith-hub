using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class DirectCustomerConfiguration : IEntityTypeConfiguration<DirectCustomer>
{
    public void Configure(EntityTypeBuilder<DirectCustomer> builder)
    {
        builder.ToTable("DirectCustomers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(5);
        builder.Property(c => c.Phone).HasMaxLength(15);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Address).IsRequired();
        builder.Property(c => c.SiretOrSiren).HasMaxLength(15);
        builder.Property(c => c.PaymentDelay).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();
        builder
            .HasOne<Freelance>()
            .WithMany()
            .HasForeignKey(c => c.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Currency>()
            .WithMany()
            .HasForeignKey(c => c.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Status>()
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
