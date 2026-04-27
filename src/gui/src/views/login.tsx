import FormContainer from "../components/ui/FormContainer";
import FormInputGroup from "../components/ui/FormInputGroup";

function Login() {
    return (
        <div className="authentication">
            <h1 className="invisible">Connexion</h1>
            <FormContainer title="Bienvenue" presentation="Connectez-vous pour accéder à votre tableau de bord" button_name="Se connecter" link={{ link_message: "Pas encore de compte ?", link_destination: "/signup", link_text: "S'inscrire" }} onSubmit={(e) => { e.preventDefault(); alert("Form Submitted!"); }}>
                <FormInputGroup label="E-mail&nbsp;*" type="email" name="email" placeholder="jean.dupont@exemple.com" />
                <FormInputGroup label="Mot de passe&nbsp;*" type="password" name="password" placeholder="************" />
            </FormContainer>
        </div>
    );
}

export default Login;