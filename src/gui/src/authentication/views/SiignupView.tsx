import { use, useState } from "react";
import * as zod from "zod";
import CheckboxOption from "../../components/ui/CheckboxOption";
import FormContainer from "../../components/ui/FormContainer";
import FormInputGroup from "../../components/ui/FormInputGroup";
import "../../stylesheets/authentication-form.css";
import SignupUser from "../models/SignupUser";
import { signupSchema } from "../zod/authenticationSchemas";
import axios from "axios";
import { createApiClient } from "../../infrastructure/openApi/client";
import { useNavigate } from "react-router";

function SignupView() {
    const [conditionsIsChecked, setConditionsIsChecked] = useState(false);
    const [privacyIsChecked, setPrivacyIsChecked] = useState(false);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const navigate = useNavigate();
    const apiClient = createApiClient(import.meta.env.VITE_API_BASE_URL || "https://localhost:7095");

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const userData = new SignupUser(
            formData.get("email") as string,
            formData.get("password") as string,
            formData.get("password_confirmation") as string,
            formData.get("firstname") as string,
            formData.get("lastname") as string
        );

        const validationResult = signupSchema.safeParse({
            ...userData,
            conditions: formData.get("conditions") === "on",
            privacy: formData.get("privacy") === "on",
        });
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.RegisterUserEndpoint({ body: { ...userData } });
            navigate("/login");
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                setFieldErrors(error.response.data.errors || {});
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
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
                <CheckboxOption label="J'accepte les conditions d'utilisation" name="conditions" checked={conditionsIsChecked} required={true} onChange={setConditionsIsChecked} error={fieldErrors.conditions?.[0]} />
                <CheckboxOption label="J'accepte la politique de confidentialité" name="privacy" checked={privacyIsChecked} required={true} onChange={setPrivacyIsChecked} error={fieldErrors.privacy?.[0]} />
            </FormContainer>
        </main>
    );
}

export default SignupView;
