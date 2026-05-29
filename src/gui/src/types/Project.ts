import * as zod from "zod";

export type Project = {
    name: string;
    directCustomerId: string;
    domain: string;
    endCustomerName: string;
    description: string | null;

}

export const projectSchema = zod.object({
    name: zod
        .string()
        .min(1, { message: "Le nom du projet est requis" }),
    directCustomerId: zod
        .string()
        .min(1, { message: "Le client du projet est requis" }),
    domain: zod
        .string()
        .min(1, { message: "Le domaine du projet est requis" }),
    endCustomerName: zod
        .string()
        .min(1, { message: "Le client final du projet est requis" }),
    description: zod
        .string()
        .max(1000, { message: "La description du projet ne peut pas dépasser 1000 caractères" })
        .nullable(),
});