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
            .trim()
            .min(1, { "message": "Le libellé est requis." })
            .max(50, { "message": "Le libellé ne peut pas dépasser 50 caractères." }),
        bankName: zod
            .string()
            .trim()
            .min(1, { "message": "Le nom de la banque est requis." })
            .max(100, { "message": "Le nom de la banque ne peut pas dépasser 100 caractères." }),
        accountHolderName: zod
            .string()
            .trim()
            .min(1, { "message": "Le nom du titulaire est requis." })
            .max(150, { "message": "Le nom du titulaire ne peut pas dépasser 150 caractères." }),
        iban: zod
            .string()
            .trim()
            .min(1, { "message": "Le code IBAN est requis." })
            .max(34, { "message": "Le code IBAN ne peut pas dépasser 34 caractères." })
            .refine((value) => {
                const ibanRegex = /^[A-Z]{2}[0-9A-Z]{13,32}$/;
                return ibanRegex.test(value.replace(/\s+/g, ""));
            }, { "message": "Code IBAN invalide." }),
        bic: zod
            .string()
            .trim()
            .min(1, { "message": "Le code BIC est requis." })
            .max(11, { "message": "Le code BIC ne peut pas dépasser 11 caractères." })
            .refine((value) => {
                const bicRegex = /^[A-Z]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$/;
                return bicRegex.test(value.replace(/\s+/g, ""));
            }, { "message": "Code BIC invalide." }),
    });
