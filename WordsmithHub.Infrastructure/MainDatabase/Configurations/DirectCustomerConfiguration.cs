using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;

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
        builder.OwnsOne(c => c.Address, a =>
        {
            a.Property(p => p.StreetInfo).IsRequired().HasMaxLength(255).HasColumnName("Address_StreetInfo");
            a.Property(p => p.AddressComplement).HasMaxLength(255).HasColumnName("Address_Complement");
            a.Property(p => p.PostCode).IsRequired().HasMaxLength(10).HasColumnName("Address_PostCode");
            a.Property(p => p.City).IsRequired().HasMaxLength(100).HasColumnName("Address_City");
            a.Property(p => p.State).HasMaxLength(50).HasColumnName("Address_State");
            a.Property(p => p.CountryId).IsRequired().HasColumnName("Address_CountryId");
        });
        builder.Property(c => c.SiretOrSiren).HasMaxLength(15);
        builder.Property(c => c.PaymentDelay).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();
        builder
            .HasOne(c => c.Freelance)
            .WithMany()
            .HasForeignKey(c => c.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(c => c.Currency)
            .WithMany()
            .HasForeignKey(c => c.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(c => c.Status)
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Country>()
            .WithMany()
            .HasForeignKey("Address_CountryId")
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex("Address_CountryId");
    }
}