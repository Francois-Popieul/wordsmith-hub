import * as zod from "zod";

export type PersonalData = {
    firstName: string;
    lastName: string;
    email: string;
    phone: string | null;
};

export const personalDataSchema = zod.object({
    firstName: zod
        .string()
        .trim()
        .min(1, { "message": "Le prénom est requis" })
        .max(50, { "message": "Le prénom ne peut pas dépasser 50 caractères" }),
    lastName: zod
        .string()
        .trim()
        .min(1, { "message": "Le nom est requis" })
        .max(100, { "message": "Le nom ne peut pas dépasser 100 caractères" }),
    email: zod
        .email({ "message": "Email invalide" })
        .trim()
        .max(255, { "message": "L'email ne peut pas dépasser 255 caractères" }),
    phone: zod
        .string()
        .trim()
        .max(20, { "message": "Le numéro de téléphone ne peut pas dépasser 20 caractères" })
        .nullable()
        .refine(value => value === null || /^\+?[0-9\s\-()]+$/.test(value), { "message": "Numéro de téléphone invalide" }),
});