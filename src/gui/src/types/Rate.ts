import * as zod from "zod";

export type Rate = {
    unitPrice: number;
    unit: string;
    sourceLanguageId: number;
    targetLanguageId: number;
    serviceId: number;
    directCustomerId: string;
}

export const rateSchema = zod.object({
    unitPrice: zod
        .number({ message: "Le prix unitaire doit être un nombre" })
        .positive({ message: "Le prix unitaire doit être supérieur à zéro" }),
    unit: zod
        .string()
        .min(1, { message: "L'unité est requise" }),
    sourceLanguageId: zod
        .number({ message: "La langue source est requise" }),
    targetLanguageId: zod
        .number({ message: "La langue cible est requise" }),
    serviceId: zod
        .number({ message: "Le service est requis" }),
    directCustomerId: zod
        .string()
        .min(1, { message: "Le client direct est requis" })
});