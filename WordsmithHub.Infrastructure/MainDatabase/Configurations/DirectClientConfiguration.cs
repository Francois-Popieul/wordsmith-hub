using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class DirectClientConfiguration : IEntityTypeConfiguration<DirectClient>
{
    public void Configure(EntityTypeBuilder<DirectClient> builder)
    {
        builder.ToTable("DirectClients");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Address).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Phone).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
        builder.Property(c => c.PaymentDelay).IsRequired();
    }
}
