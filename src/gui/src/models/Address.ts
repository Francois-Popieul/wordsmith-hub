import * as zod from "zod";

export type Address = {
    streetInfo: string;
    addressComplement: string | null;
    postCode: string;
    city: string;
    state: string | null;
    countryId: number;

}

export const addressSchema = zod
    .object({
        streetInfo: zod
            .string()
            .min(5, "Veuillez saisir une adresse valide"),
        addressComplement: zod
            .string()
            .nullable(),
        postCode: zod
            .string()
            .trim()
            .min(1, "Code postal requis")
            .max(5, "Code postal invalide"),
        city: zod
            .string()
            .trim()
            .min(1, "Nom de ville requis"),
        state: zod
            .string()
            .nullable(),
        countryId: zod
            .number()
    })