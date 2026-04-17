using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.BankAccountAggregate;
using WordsmithHub.Domain.FreelanceAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccounts");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();
        builder.Property(a => a.Label).IsRequired().HasMaxLength(50);
        builder.Property(a => a.BankName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.AccountHolderName).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Iban).IsRequired().HasMaxLength(34);
        builder.Property(a => a.Bic).IsRequired().HasMaxLength(11);
        builder
            .HasOne<Freelance>()
            .WithMany()
            .HasForeignKey(a => a.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Status>()
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
