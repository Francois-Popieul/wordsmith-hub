using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordsmithHub.Domain;

namespace WordsmithHub.Infrastructure.MainDatabase.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().ValueGeneratedNever();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Code).IsRequired().HasMaxLength(3);
        builder.Property(c => c.Symbol).IsRequired().HasMaxLength(3);
        // Seed data
        builder.HasData(
            new Currency { Id = 1, Name = "Dollar américain", Code = "USD", Symbol = "$" },
            new Currency { Id = 2, Name = "Euro", Code = "EUR", Symbol = "€" },
            new Currency { Id = 3, Name = "Yen japonais", Code = "JPY", Symbol = "¥" },
            new Currency { Id = 4, Name = "Livre sterling", Code = "GBP", Symbol = "£" },
            new Currency { Id = 5, Name = "Dollar australien", Code = "AUD", Symbol = "$" },
            new Currency { Id = 6, Name = "Dollar canadien", Code = "CAD", Symbol = "$" },
            new Currency { Id = 7, Name = "Franc suisse", Code = "CHF", Symbol = "CHF" },
            new Currency { Id = 8, Name = "Yuan renminbi chinois", Code = "CNY", Symbol = "¥" },
            new Currency { Id = 9, Name = "Couronne suédoise", Code = "SEK", Symbol = "kr" },
            new Currency { Id = 10, Name = "Dollar néo‑zélandais", Code = "NZD", Symbol = "$" },
            new Currency { Id = 11, Name = "Peso mexicain", Code = "MXN", Symbol = "$" },
            new Currency { Id = 12, Name = "Dollar de Singapour", Code = "SGD", Symbol = "$" },
            new Currency { Id = 13, Name = "Dollar de Hong Kong", Code = "HKD", Symbol = "$" },
            new Currency { Id = 14, Name = "Couronne norvégienne", Code = "NOK", Symbol = "kr" },
            new Currency { Id = 15, Name = "Won sud‑coréen", Code = "KRW", Symbol = "₩" },
            new Currency { Id = 16, Name = "Livre turque", Code = "TRY", Symbol = "₺" },
            new Currency { Id = 17, Name = "Rouble russe", Code = "RUB", Symbol = "₽" },
            new Currency { Id = 18, Name = "Roupie indienne", Code = "INR", Symbol = "₹" },
            new Currency { Id = 19, Name = "Real brésilien", Code = "BRL", Symbol = "R$" },
            new Currency { Id = 20, Name = "Rand sud‑africain", Code = "ZAR", Symbol = "R" },
            new Currency { Id = 21, Name = "Couronne danoise", Code = "DKK", Symbol = "kr" },
            new Currency { Id = 22, Name = "Zloty polonais", Code = "PLN", Symbol = "zł" },
            new Currency { Id = 23, Name = "Baht thaïlandais", Code = "THB", Symbol = "฿" },
            new Currency { Id = 24, Name = "Forint hongrois", Code = "HUF", Symbol = "Ft" },
            new Currency { Id = 25, Name = "Couronne tchèque", Code = "CZK", Symbol = "Kč" },
            new Currency { Id = 26, Name = "Shekel israélien", Code = "ILS", Symbol = "₪" },
            new Currency { Id = 27, Name = "Peso philippin", Code = "PHP", Symbol = "₱" },
            new Currency { Id = 28, Name = "Ringgit malaisien", Code = "MYR", Symbol = "RM" },
            new Currency { Id = 29, Name = "Dirham des Émirats arabes unis", Code = "AED", Symbol = "د.إ" },
            new Currency { Id = 30, Name = "Riyal saoudien", Code = "SAR", Symbol = "﷼" },
            new Currency { Id = 31, Name = "Dinar koweïtien", Code = "KWD", Symbol = "KD" },
            new Currency { Id = 32, Name = "Dinar bahreïni", Code = "BHD", Symbol = "BD" },
            new Currency { Id = 33, Name = "Peso argentin", Code = "ARS", Symbol = "$" },
            new Currency { Id = 34, Name = "Naira nigérian", Code = "NGN", Symbol = "₦" },
            new Currency { Id = 35, Name = "Shilling kényan", Code = "KES", Symbol = "KSh" }
        );
    }
}
