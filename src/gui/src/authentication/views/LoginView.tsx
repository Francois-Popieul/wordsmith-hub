import zod from "zod";
import FormContainer from "../../components/ui/FormContainer";
import FormInputGroup from "../../components/ui/FormInputGroup";
import "../../stylesheets/authentication-form.css";
import { useState } from "react";
import type LoginUser from "../models/LoginUser";

const loginSchema = zod
    .object({
        email: zod.email("Veuillez renseigner une adresse e-mail valide."),
        password: zod.string().min(1, "Le mot de passe est requis."),
    });

function LoginView() {
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const userData: LoginUser = {
            email: formData.get("email") as string,
            password: formData.get("password") as string
        };

        const validationResult = loginSchema.safeParse(userData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});
        console.log("User Data:", userData);
    }
    return (
        <main className="authentication">
            <h1 className="invisible">Connexion</h1>
            <FormContainer title="Bienvenue" presentation="Connectez-vous pour accéder à votre tableau de bord" button_name="Se connecter" link={{ link_message: "Pas encore de compte ?", link_destination: "/signup", link_text: "S'inscrire" }} onSubmit={handleSubmit}>
                <FormInputGroup label="E-mail" type="email" name="email" placeholder="jean.dupont@exemple.com" error={fieldErrors.email?.[0]} />
                <FormInputGroup label="Mot de passe" type="password" name="password" placeholder="************" error={fieldErrors.password?.[0]} />
            </FormContainer>
        </main>
    );
}

export default LoginView;