import * as zod from "zod";

export const signupSchema = zod
    .object({
        firstName: zod.string().trim().max(50, { message: "Prénom trop long." }).optional(),
        lastName: zod.string().trim().max(100, { message: "Nom trop long." }).optional(),
        email: zod.email({ message: "Veuillez renseigner une adresse e-mail valide." }),
        password: zod.string().regex(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$/, { message: "Le mot de passe doit contenir au moins 12 caractères incluant majuscules, minuscules, chiffres et caractères spéciaux." }),
        passwordConfirmation: zod.string(),
        conditions: zod.literal(true, { message: "Vous devez accepter les conditions d’utilisation." }),
        privacy: zod.literal(true, { message: "Vous devez accepter la politique de confidentialité." }),
    })
    .refine(({ password, passwordConfirmation }) => password === passwordConfirmation, {
        path: ["passwordConfirmation"],
        message: "Les mots de passe ne correspondent pas.",
    });

export const loginSchema = zod
    .object({
        email: zod.email({ message: "Veuillez renseigner une adresse e-mail valide." }),
        password: zod.string().min(1, { message: "Le mot de passe est requis." }),
    });