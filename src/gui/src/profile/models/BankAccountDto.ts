export default class BankAccountDto {
    id: string;
    label: string;
    bankName: string;
    accountHolderName: string;
    iban: string;
    bic: string;
    isDefault: boolean;

    public constructor(
        id: string,
        label: string,
        bankName: string,
        accountHolderName: string,
        iban: string,
        bic: string,
        isDefault: boolean
    ) {
        this.id = id;
        this.label = label;
        this.bankName = bankName;
        this.accountHolderName = accountHolderName;
        this.iban = iban;
        this.bic = bic;
        this.isDefault = isDefault;
    }
}