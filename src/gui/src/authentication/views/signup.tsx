import { useState } from "react";
import * as zod from "zod";
import CheckboxOption from "../../components/ui/CheckboxOption";
import FormContainer from "../../components/ui/FormContainer";
import FormInputGroup from "../../components/ui/FormInputGroup";
import "../../stylesheets/authentication-form.css";
import type SignupUser from "../models/SignupUser";

const signupSchema = zod
    .object({
        firstName: zod.string().trim().max(50, "Prénom trop long.").optional(),
        lastName: zod.string().trim().max(100, "Nom trop long.").optional(),
        email: zod.email("Veuillez renseigner une adresse e-mail valide."),
        password: zod.string().min(12, "Le mot de passe doit contenir au moins 12 caractères incluant majuscules, minuscules, chiffres et caractères spéciaux."),
        passwordConfirmation: zod.string(),
        conditions: zod.literal(true, { message: "Vous devez accepter les conditions d’utilisation." }),
        privacy: zod.literal(true, { message: "Vous devez accepter la politique de confidentialité." }),
    })
    .refine(({ password, passwordConfirmation }) => password === passwordConfirmation, {
        path: ["passwordConfirmation"],
        message: "Les mots de passe ne correspondent pas.",
    });

function Signup() {
    const [conditionsIsChecked, setConditionsIsChecked] = useState(false);
    const [privacyIsChecked, setPrivacyIsChecked] = useState(false);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const userData: SignupUser = {
            firstName: formData.get("firstname") as string | null,
            lastName: formData.get("lastname") as string | null,
            email: formData.get("email") as string,
            password: formData.get("password") as string,
            passwordConfirmation: formData.get("password_confirmation") as string,
        };

        const validationResult = signupSchema.safeParse(userData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});
        console.log("User Data:", userData);
    }

    return (
        <main className="authentication">
            <h1 className="invisible">Inscription</h1>
            <FormContainer title="Créer votre compte" presentation="Commencez à gérer votre activité de traduction" button_name="Créer un compte" link={{ link_message: "Vous avez déjà un compte ?", link_destination: "/login", link_text: "Se connecter" }} onSubmit={handleSubmit}>
                <FormInputGroup label="Prénom" type="text" name="firstname" placeholder="Jean" required={false} error={fieldErrors.firstName?.[0]} />
                <FormInputGroup label="Nom" type="text" name="lastname" placeholder="Dupont" required={false} error={fieldErrors.lastName?.[0]} />
                <FormInputGroup label="E-mail" type="email" name="email" placeholder="jean.dupont@exemple.com" error={fieldErrors.email?.[0]} />
                <FormInputGroup label="Mot de passe" type="password" name="password" placeholder="************" error={fieldErrors.password?.[0]} />
                <FormInputGroup label="Confirmation du mot de passe" type="password" name="password_confirmation" placeholder="************" error={fieldErrors.passwordConfirmation?.[0]} />
                <CheckboxOption label="J’accepte les conditions d’utilisation" checked={conditionsIsChecked} required={true} onChange={setConditionsIsChecked} error={fieldErrors.conditions?.[0]} />
                <CheckboxOption label="J’accepte la politique de confidentialité" checked={privacyIsChecked} required={true} onChange={setPrivacyIsChecked} error={fieldErrors.privacy?.[0]} />
            </FormContainer>
        </main>
    );
}

export default Signup;