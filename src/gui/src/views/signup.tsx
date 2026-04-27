import FormContainer from "../components/ui/FormContainer";
import FormInputGroup from "../components/ui/FormInputGroup";

function Signup() {
    return (
        <div className="authentication">
            <h1 className="invisible">Inscription</h1>
            <FormContainer title="Créer votre compte" presentation="Commencez à gérer votre activité de traduction" button_name="Créer un compte" link={{ link_message: "Vous avez déjà un compte ?", link_destination: "/login", link_text: "Se connecter" }} onSubmit={(e) => { e.preventDefault(); alert("Form Submitted!"); }}>
                <FormInputGroup label="Prénom" type="text" name="firstname" placeholder="Jean" required={false} />
                <FormInputGroup label="Nom" type="text" name="lastname" placeholder="Dupont" required={false} />
                <FormInputGroup label="E-mail&nbsp;*" type="email" name="email" placeholder="jean.dupont@exemple.com" />
                <FormInputGroup label="Mot de passe&nbsp;*" type="password" name="password" placeholder="************" />
                <FormInputGroup label="Confirmation du mot de passe&nbsp;*" type="password" name="password_confirmation" placeholder="************" />

            </FormContainer>
        </div>
    );
}

export default Signup;