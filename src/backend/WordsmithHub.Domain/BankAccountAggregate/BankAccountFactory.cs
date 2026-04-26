namespace WordsmithHub.Domain.BankAccountAggregate;

public interface IBankAccountFactory
{
    BankAccount CreateBankAccount(
        Guid freelanceId,
        string label,
        string bankName,
        string accountHolderName,
        string iban,
        string bic,
        bool isDefault
    );
}

public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount CreateBankAccount(Guid freelanceId,
        string label,
        string bankName,
        string accountHolderName,
        string iban,
        string bic,
        bool isDefault)
    {
        var bankAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            Label = label,
            BankName = bankName,
            AccountHolderName = accountHolderName,
            Iban = iban,
            Bic = bic,
            IsDefault = isDefault,
            FreelanceId = freelanceId,
            StatusId = StatusIds.General.Active,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return bankAccount;
    }
}
