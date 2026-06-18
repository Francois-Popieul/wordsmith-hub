import * as zod from "zod";

export const addressSchema = zod
    .object({
        streetInfo: zod
            .string()
            .trim()
            .min(5, { "message": "Veuillez saisir une adresse valide" })
            .max(255, { "message": "L’adresse ne doit pas dépasser 255 caractères" }),
        addressComplement: zod
            .string()
            .trim()
            .max(255, { "message": "Le complément d’adresse ne doit pas dépasser 255 caractères" })
            .nullable(),
        postCode: zod
            .string()
            .trim()
            .min(3, { "message": "Code postal d’au moins 3 caractères requis" })
            .max(10, { "message": "Code postal invalide" }),
        city: zod
            .string()
            .trim()
            .min(1, { "message": "Nom de ville requis" })
            .max(100, { "message": "Nom de ville invalide" }),
        state: zod
            .string()
            .trim()
            .max(50, { "message": "Nom d’état invalide" })
            .nullable(),
        countryId: zod
            .number()
    })