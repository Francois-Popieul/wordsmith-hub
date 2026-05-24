import * as zod from "zod";

export type PersonalData = {
    firstName: string;
    lastName: string;
    email: string;
    phone: string | null;
};

export const personalDataSchema = zod.object({
    firstName: zod.string().min(1, { "message": "Le prénom est requis" }).max(50, { "message": "Le prénom ne peut pas dépasser 50 caractères" }),
    lastName: zod.string().min(1, { "message": "Le nom est requis" }).max(100, { "message": "Le nom ne peut pas dépasser 100 caractères" }),
    email: zod.email({ "message": "Email invalide" }),
    phone: zod.string().max(15, { "message": "Le numéro de téléphone ne peut pas dépasser 15 caractères" }).nullable()
});