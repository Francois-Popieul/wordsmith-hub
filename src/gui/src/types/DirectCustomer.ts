import type { Address } from "./Address";
import { addressSchema } from "./Address";
import * as zod from "zod";

export type DirectCustomer = {
    name: string;
    code: string;
    email: string;
    phone: string | null;
    address: Address;
    siretOrSiren: string | null;
    paymentDelay: number;
    currencyId: number;
}

export const directCustomerSchema = zod.object({
    name: zod.string().min(1, { message: "Le nom du client est requis" }),
    code: zod.string().min(1, { message: "Le code du client est requis" }),
    email: zod.email({ message: "L’email du client doit être valide" }),
    phone: zod.string().nullable(),
    address: addressSchema,
    siretOrSiren: zod.string().nullable(),
    paymentDelay: zod
        .number()
        .int({ message: "Le délai de paiement doit être un nombre entier" })
        .positive({ message: "Le délai de paiement doit être un nombre positif" }),
    currencyId: zod
        .number()
        .int({ message: "La devise doit être un nombre entier" })
        .positive({ message: "La devise doit être un nombre positif" }),
});