import type { Address } from "./Address";
import { addressSchema } from "./Address";
import * as zod from "zod";

export type DirectCustomer = {
    name: string;
    code: string;
    email: string;
    phone: string | null;
    address: Address;
    siret: string | null;
    paymentDelay: string;
    currencyId: string;
}

export const directCustomerSchema = zod.object({
    name: zod.string().min(1, { message: "Le nom du client est requis" }),
    code: zod.string().min(1, { message: "Le code du client est requis" }),
    email: zod.email({ message: "L’email du client doit être valide" }),
    phone: zod.string().nullable(),
    address: addressSchema,
    siret: zod.string().nullable(),
    paymentDelay: zod
        .string()
        .refine((value) => /^\d+$/.test(value), { message: "Le délai de paiement doit être un nombre entier positif" }),
    currencyId: zod
        .string()
        .min(1, { message: "La devise est requise" })
        .refine((value) => /^\d+$/.test(value), { message: "Devise non valide" }),
});