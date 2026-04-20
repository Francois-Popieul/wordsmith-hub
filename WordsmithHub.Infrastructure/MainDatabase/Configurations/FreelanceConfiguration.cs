using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class FreelanceConfiguration : IEntityTypeConfiguration<Freelance>
{
    public void Configure(EntityTypeBuilder<Freelance> builder)
    {
        builder.ToTable("Freelances");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();
        builder.Property(f => f.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(f => f.LastName).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Email).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Phone).HasMaxLength(15);
        builder.OwnsOne(f => f.Address, a =>
        {
            a.Property(p => p.StreetInfo).IsRequired().HasMaxLength(255).HasColumnName("Address_StreetInfo");
            a.Property(p => p.AddressComplement).HasMaxLength(255).HasColumnName("Address_Complement");
            a.Property(p => p.PostCode).IsRequired().HasMaxLength(10).HasColumnName("Address_PostCode");
            a.Property(p => p.City).IsRequired().HasMaxLength(100).HasColumnName("Address_City");
            a.Property(p => p.State).HasMaxLength(50).HasColumnName("Address_State");
            a.Property(p => p.CountryId).IsRequired().HasColumnName("Address_CountryId");
        });
        builder.Property(f => f.AppUserId).IsRequired();
        builder.Property(f => f.CreatedAt).IsRequired();
        builder.Property(f => f.UpdatedAt).IsRequired();
        builder
            .HasOne(f => f.Status)
            .WithMany()
            .HasForeignKey(f => f.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Country>()
            .WithMany()
            .HasForeignKey("Address_CountryId")
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex("Address_CountryId");
    }
}
