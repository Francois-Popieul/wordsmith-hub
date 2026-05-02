import zod from "zod";
import AuthFormContainer from "../../components/ui/AuthFormContainer";
import FormInputGroup from "../../components/ui/FormInputGroup";
import "../../stylesheets/authentication-form.css";
import { useState } from "react";
import LoginUser from "../models/LoginUser";
import { loginSchema } from "../zod/authenticationSchemas";
import { createApiClient } from "../../infrastructure/openApi/client";
import { Link, useNavigate } from "react-router";
import axios from "axios";


function LoginView() {
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const apiClient = createApiClient(import.meta.env.VITE_API_BASE_URL);
    const navigate = useNavigate();

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const userData: LoginUser = new LoginUser(
            formData.get("email") as string,
            formData.get("password") as string
        );

        const validationResult = loginSchema.safeParse(userData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});
        console.log("User Data:", userData);

        try {
            const response = await apiClient.LoginUserEndpoint({ body: { ...userData } });
            localStorage.setItem("wshToken", response.accessToken);
            navigate("/dashboard");
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
            <h1 className="invisible">Connexion</h1>
            <AuthFormContainer title="Bienvenue" presentation="Connectez-vous pour accéder à votre tableau de bord" button_name="Se connecter" link={{ link_message: "Pas encore de compte ?", link_destination: "/signup", link_text: "S’inscrire" }} onSubmit={handleSubmit}>
                <FormInputGroup label="E-mail" type="email" name="email" placeholder="jean.dupont@exemple.com" error={fieldErrors.email?.[0]} />
                <FormInputGroup label="Mot de passe" type="password" name="password" placeholder="************" error={fieldErrors.password?.[0]} />
                <Link to="/forgot-password"><div className="forgotten_password_container"><p className="forgotten_password">Mot de passe oublié&nbsp;?</p></div></Link>
            </AuthFormContainer>
        </main>
    );
}

export default LoginView;
