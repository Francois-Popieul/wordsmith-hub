export default class LegalStatusDto {
    id: string;
    name: string;
    siret: string | null;
    vatNumber: string | null;
    vatExemption: boolean;
    vatRate: number | null;
    taxDeductionExemption: boolean;
    validFrom: string;
    validTo: string | null;

    public constructor(
        id: string,
        name: string,
        siret: string | null,
        vatNumber: string | null,
        vatExemption: boolean,
        vatRate: number | null,
        taxDeductionExemption: boolean,
        validFrom: string,
        validTo: string | null
    ) {
        this.id = id;
        this.name = name;
        this.siret = siret;
        this.vatNumber = vatNumber;
        this.vatExemption = vatExemption;
        this.vatRate = vatRate;
        this.taxDeductionExemption = taxDeductionExemption;
        this.validFrom = validFrom;
        this.validTo = validTo;
    }
}