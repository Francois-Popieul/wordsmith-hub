import { Link } from "react-router";
import "./Footer.css";
import Brand from "./Brand";

function Footer() {
    return (
        <footer className="homepage_footer">
            <Brand variant="light" width="small" />
            <div className="link_container">
                <p className="slogan">La plateforme moderne pour les traducteurs indépendants.</p>
                <Link to="/terms_of_service" className="no_decoration link_color footer_link">Conditions d’utilisation</Link>
                <Link to="/privacy_policy" className="no_decoration link_color footer_link">Politique de confidentialité</Link>
            </div>
        </footer>
    );
}

export default Footer;