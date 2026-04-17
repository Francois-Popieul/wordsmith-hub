namespace WordsmithHub.Domain.BankAccountAggregate;

public interface IBankAccountFactory
{
    BankAccount CreateBankAccount(
        Guid freelanceId,
        string label,
        string bankName,
        string accountHolderName,
        string iban,
        string bic
    );
}

public class BankAccountFactory : IBankAccountFactory
{
    public BankAccount CreateBankAccount(Guid freelanceId,
        string label,
        string bankName,
        string accountHolderName,
        string iban,
        string bic)
    {
        var bankAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            Label = label,
            BankName = bankName,
            AccountHolderName = accountHolderName,
            Iban = iban,
            Bic = bic,
            FreelanceId = freelanceId,
            StatusId = 1,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        return bankAccount;
    }
}
