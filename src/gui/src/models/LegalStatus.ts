import * as zod from "zod";

export type LegalStatus = {
    name: string,
    siret: string | null,
    vatNumber: string | null,
    vatExemption: boolean,
    vatRate: string | null,
    taxDeductionExemption: boolean,
    validFrom: string,
    validTo: string | null,
};

export const legalStatusSchema = zod.object({
    name: zod
        .string()
        .min(1, "Le nom du statut juridique est requis")
        .max(100, "Le nom du statut juridique ne peut pas dépasser 100 caractères."),
    siret: zod
        .string()
        .nullable()
        .refine(value => value === null || /^\d{14}$/.test(value), "Le numéro SIRET doit comporter exactement 14 chiffres."),
    vatNumber: zod
        .string()
        .nullable()
        .refine(value => value === null || /^[A-Z]{2}[A-Z0-9]{2,}$/.test(value), "Le numéro de TVA doit commencer par un code pays suivi d'au moins 2 caractères alphanumériques."),
    vatExemption: zod.boolean(),
    vatRate: zod
        .string()
        .nullable()
        .refine(value => value === null || (/^\d+(\.\d+)?$/.test(value) && parseFloat(value) >= 0 && parseFloat(value) <= 100), "Le taux de TVA doit être compris entre 0 et 100."),
    taxDeductionExemption: zod
        .boolean(),
    validFrom: zod
        .string()
        .refine(value => /^\d{4}-\d{2}-\d{2}$/.test(value), "La date de validité doit être au format YYYY-MM-DD."),
    validTo: zod
        .string()
        .nullable()
        .refine(value => value === null || /^\d{4}-\d{2}-\d{2}$/.test(value), "La date de validité doit être au format YYYY-MM-DD."),
});