import Button from "../components/ui/Button";
import { Link } from "react-router";

function Homepage() {
    return (
        <div className="homepage">
            <h1 className="invisible">Page d'accueil de Wordsmith Hub</h1>
            <div className="homepage_header">
                <div className="homepage_brand_container">
                    <img src="" alt="" />
                    <p>Wordsmith Hub</p>
                </div>
                <div className="homepage_button_container">
                    <Link to={"/login"}><Button name="Se connecter" variant="light" width="medium" /></Link>
                    <Link to={"/signup"}><Button name="Commencer" variant="dark" width="medium" /></Link>
                </div>
            </div>

            <div className="homepage_presentation_container">
                <div>
                    <h2>Simplifiez la gestion de votre activité de traduction</h2>
                    <p>Wordsmith Hub est un service complet destiné aux traducteurs indépendants. Gérez vos clients, suivez vos projets et créez vos factures au même endroit.</p>
                </div>
                <img src="" alt="" />
            </div>

            <div className="homepage_tools_container">
                <h2>Tout ce qu'il vous faut pour réussir</h2>
                <p>Des outils pratiques conçus pour les traducteurs indépendants</p>
                <div className="homepage_vignette_container">

                </div>
            </div>

            <div className="homepage_feature_container">
                <img src="" alt="" />
                <div>
                    <h2>Concentrez-vous sur la traduction, pas sur la gestion administrative</h2>
                    <ul>
                        <li>Gagnez du temps</li>
                        <li>Faites-vous payer plus vite</li>
                        <li>Développez votre activité</li>
                        <li>Optimisez votre organisation</li>
                    </ul>
                </div>
            </div>

        </div>
    );
}

export default Homepage;