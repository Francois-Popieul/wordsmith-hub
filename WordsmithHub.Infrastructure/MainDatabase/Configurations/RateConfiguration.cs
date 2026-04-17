using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;
using WordsmithHub.Domain.DirectCustomerAggregate;
using WordsmithHub.Domain.FreelanceAggregate;
using WordsmithHub.Domain.RateAggregate;
using WordsmithHub.Domain.WorkOrderAggregate;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class RateConfiguration : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        builder.ToTable("Rates");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.UnitPrice).IsRequired().HasColumnType("decimal(10,4)");
        builder.Property(r => r.Unit).IsRequired().HasMaxLength(20);
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.Property(r => r.UpdatedAt).IsRequired();
        builder
            .HasOne<TranslationLanguage>()
            .WithMany()
            .HasForeignKey(r => r.SourceLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<TranslationLanguage>()
            .WithMany()
            .HasForeignKey(r => r.TargetLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Service>()
            .WithMany()
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Status>()
            .WithMany()
            .HasForeignKey(r => r.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<DirectCustomer>()
            .WithMany()
            .HasForeignKey(r => r.DirectCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne<Freelance>()
            .WithMany()
            .HasForeignKey(r => r.FreelanceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
