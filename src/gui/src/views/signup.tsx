import { useState } from "react";
import CheckboxOption from "../components/ui/CheckboxOption";
import FormContainer from "../components/ui/FormContainer";
import FormInputGroup from "../components/ui/FormInputGroup";
import "../stylesheets/authentication-form.css";

function Signup() {
    const [conditionsIsChecked, setConditionsIsChecked] = useState(false);
    const [privacyIsChecked, setPrivacyIsChecked] = useState(false);

    async function handleSubmit(event: React.SubmitEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        console.log("Form Data:", Object.fromEntries(formData.entries()));
    }

    return (
        <main className="authentication">
            <h1 className="invisible">Inscription</h1>
            <FormContainer title="Créer votre compte" presentation="Commencez à gérer votre activité de traduction" button_name="Créer un compte" link={{ link_message: "Vous avez déjà un compte ?", link_destination: "/login", link_text: "Se connecter" }} onSubmit={handleSubmit}>
                <FormInputGroup label="Prénom" type="text" name="firstname" placeholder="Jean" required={false} />
                <FormInputGroup label="Nom" type="text" name="lastname" placeholder="Dupont" required={false} />
                <FormInputGroup label="E-mail" type="email" name="email" placeholder="jean.dupont@exemple.com" />
                <FormInputGroup label="Mot de passe" type="password" name="password" placeholder="************" />
                <FormInputGroup label="Confirmation du mot de passe" type="password" name="password_confirmation" placeholder="************" />
                <CheckboxOption label="J’accepte les conditions d’utilisation" checked={conditionsIsChecked} required={true} onChange={setConditionsIsChecked} />
                <CheckboxOption label="J’accepte la politique de confidentialité" checked={privacyIsChecked} required={true} onChange={setPrivacyIsChecked} />
            </FormContainer>
        </main>
    );
}

export default Signup;