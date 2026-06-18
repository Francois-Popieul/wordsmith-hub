import { useState } from "react";
import * as zod from "zod";
import CheckboxOption from "../../components/ui/CheckboxOption";
import AuthFormContainer from "../../components/ui/AuthFormContainer";
import FormInputGroup from "../../components/ui/FormInputGroup";
import "../../components/ui/AuthFormContainer.css";
import { signupSchema } from "../zod/authenticationSchemas";
import axios from "axios";
import { createApiClient, schemas } from "../../infrastructure/openApi/client";
import { useNavigate } from "react-router";
import { useToast } from "../../hooks/useToast/useToast";

function SignupView() {
    const [conditionsIsChecked, setConditionsIsChecked] = useState(false);
    const [privacyIsChecked, setPrivacyIsChecked] = useState(false);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const navigate = useNavigate();
    const apiClient = createApiClient(import.meta.env.VITE_API_BASE_URL);

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const userData: zod.infer<typeof schemas.RegisterUserRequest> = {
            firstName: formData.get("firstname") as string,
            lastName: formData.get("lastname") as string,
            email: formData.get("email") as string,
            password: formData.get("password") as string,
            passwordConfirmation: formData.get("password_confirmation") as string
        };

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
            addToast("success", "Inscription réussie. Connectez-vous pour continuer.", "top_right", 3000);
            navigate("/login");
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite.", "top_right", 3000);
            }
        }
    }

    return (
        <main className="authentication">
            <h1 className="visually-hidden">Page d’inscription</h1>
            <AuthFormContainer title="Créer votre compte" presentation="Commencez à gérer votre activité de traduction" button_name="Créer un compte" link={{ link_message: "Vous avez déjà un compte ?", link_destination: "/login", link_text: "Se connecter" }} onSubmit={handleSubmit}>
                <FormInputGroup label="Prénom" type="text" name="firstname" placeholder="Jean" required={false} error={fieldErrors.firstName?.[0]} />
                <FormInputGroup label="Nom" type="text" name="lastname" placeholder="Dupont" required={false} error={fieldErrors.lastName?.[0]} />
                <FormInputGroup label="E-mail" type="email" name="email" placeholder="jean.dupont@exemple.com" error={fieldErrors.email?.[0]} />
                <FormInputGroup label="Mot de passe" type="password" name="password" placeholder="************" error={fieldErrors.password?.[0]} />
                <FormInputGroup label="Confirmation du mot de passe" type="password" name="password_confirmation" placeholder="************" error={fieldErrors.passwordConfirmation?.[0]} />
                <CheckboxOption
                    label={<>J’accepte les <a href="/terms_of_service">conditions d’utilisation</a></>}
                    name="conditions"
                    checked={conditionsIsChecked}
                    required={true}
                    onChange={setConditionsIsChecked}
                    error={fieldErrors.conditions?.[0]} />
                <CheckboxOption
                    label={<>J’accepte la <a href="/privacy_policy">politique de confidentialité</a></>}
                    name="privacy"
                    checked={privacyIsChecked}
                    required={true}
                    onChange={setPrivacyIsChecked}
                    error={fieldErrors.privacy?.[0]} />
            </AuthFormContainer>
        </main>
    );
}

export default SignupView;
