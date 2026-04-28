import { Link } from "react-router";
import "./Footer.css";
import Brand from "./Brand";

function Footer() {
    return (
        <footer className="homepage_footer">
            <Brand variant="light" width="small" />
            <div className="link_container">
                <p className="slogan">La plateforme moderne pour les traducteurs indépendants.</p>
                <Link to="/terms" className="no_decoration"><p className="link_color">Conditions d'utilisation</p></Link>
                <Link to="/privacy" className="no_decoration"><p className="link_color">Politique de confidentialité</p></Link>
            </div>
        </footer>
    );
}

export default Footer;