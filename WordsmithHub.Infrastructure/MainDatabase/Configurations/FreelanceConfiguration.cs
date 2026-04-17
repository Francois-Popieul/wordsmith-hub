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
        builder.Property(f => f.Address).IsRequired();
        builder.Property(f => f.AppUserId).IsRequired();
        builder
            .HasOne<Status>()
            .WithMany()
            .HasForeignKey(f => f.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
