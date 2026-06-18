import { addressSchema } from "./Address";
import * as zod from "zod";

export const directCustomerSchema = zod.object({
    name: zod
        .string()
        .trim()
        .min(1, { message: "Le nom du client est requis" })
        .max(150, { message: "Le nom du client ne doit pas dépasser 150 caractères" }),
    code: zod
        .string()
        .trim()
        .min(1, { message: "Le code du client est requis" })
        .max(5, { message: "Le code du client ne doit pas dépasser 5 caractères" }),
    email: zod
        .email({ message: "L’email du client doit être valide" })
        .trim()
        .max(255, { message: "L’email du client ne doit pas dépasser 255 caractères" }),
    phone: zod
        .string()
        .trim()
        .max(20, { message: "Le numéro de téléphone ne doit pas dépasser 20 caractères" })
        .nullable()
        .refine((value) => {
            if (value === null) return true;
            return /^\+?\d{1,20}$/.test(value);
        }, { message: "Le numéro de téléphone doit être valide" }),
    address: addressSchema,
    siretOrSiren: zod
        .string()
        .trim()
        .nullable()
        .refine((value) => {
            if (value === null) return true;
            return /^\d{9}$/.test(value) || /^\d{14}$/.test(value);
        }, { message: "Le SIRET ou SIREN doit être composé de 9 ou 14 chiffres" }),
    paymentDelay: zod
        .number()
        .int({ message: "Le délai de paiement doit être un nombre entier" })
        .positive({ message: "Le délai de paiement doit être un nombre positif" })
        .refine((value) => { return Number.isInteger(value) }, { message: "Le délai de paiement doit être un nombre entier" }),
    currencyId: zod
        .number()
        .int({ message: "La devise doit être un nombre entier" })
        .positive({ message: "La devise doit être un nombre positif" }),
});