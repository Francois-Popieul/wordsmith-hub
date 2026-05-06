import * as zod from "zod";

export type PersonalData = {
    firstName: string;
    lastName: string;
    email: string;
    phone: string | null;
};

export const PersonalDataSchema = zod.object({
    firstName: zod.string().min(1, "Le prénom est requis").max(50, "Le prénom ne peut pas dépasser 50 caractères"),
    lastName: zod.string().min(1, "Le nom est requis").max(100, "Le nom ne peut pas dépasser 100 caractères"),
    email: zod.email("Email invalide"),
    phone: zod.string().max(15, "Le numéro de téléphone ne peut pas dépasser 15 caractères").nullable()
});