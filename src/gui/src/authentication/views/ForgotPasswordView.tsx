import AuthFormContainer from "../../components/ui/AuthFormContainer";
import FormInputGroup from "../../components/ui/FormInputGroup";
import "../../components/ui/AuthFormContainer.css";
import { useState } from "react";
import { useToast } from "../../hooks/useToast/useToast";


function ForgotPasswordView() {
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        // TODO: Implémenter la logique de récupération de mot de passe
        event.preventDefault();
        setFieldErrors({});
        addToast("information", "Fonctionnalité de récupération de mot de passe en cours de développement.", "top_right", 3000);

    }
    return (
        <main className="authentication">
            <h1 className="visually-hidden">Page de récupération de mot de passe</h1>
            <AuthFormContainer title="Récupération de mot de passe" presentation="Entrez votre adresse e-mail pour recevoir un lien de réinitialisation" button_name="Envoyer le lien" onSubmit={handleSubmit}>
                <FormInputGroup label="E-mail" type="email" name="email" placeholder="jean.dupont@exemple.com" error={fieldErrors.email?.[0]} />
            </AuthFormContainer>
        </main>
    );
}

export default ForgotPasswordView;
