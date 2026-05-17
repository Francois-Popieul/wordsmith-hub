import * as zod from "zod";

export type BankAccount = {
    label: string;
    bankName: string;
    accountHolderName: string;
    iban: string;
    bic: string;
};

export const bankAccountSchema = zod
    .object({
        label: zod
            .string()
            .min(1, { "message": "Le libellé est requis." })
            .max(100, { "message": "Le libellé ne peut pas dépasser 100 caractères." }),
        bankName: zod
            .string()
            .min(1, { "message": "Le nom de la banque est requis." })
            .max(100, { "message": "Le nom de la banque ne peut pas dépasser 100 caractères." }),
        accountHolderName: zod
            .string()
            .min(1, { "message": "Le nom du titulaire est requis." })
            .max(100, { "message": "Le nom du titulaire ne peut pas dépasser 100 caractères." }),
        iban: zod
            .string()
            .min(1, { "message": "Le code IBAN est requis." })
            .max(34, { "message": "Le code IBAN ne peut pas dépasser 34 caractères." }),
        bic: zod
            .string()
            .min(1, { "message": "Le code BIC est requis." })
            .max(11, { "message": "Le code BIC ne peut pas dépasser 11 caractères." }),
    });
