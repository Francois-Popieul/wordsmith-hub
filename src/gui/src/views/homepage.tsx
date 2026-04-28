import Footer from "../components/partials/Footer";
import Header from "../components/partials/Header";
import HomepageFeature from "../components/ui/HomepageFeature";
import HomepageVignette from "../components/ui/HomepageVignette";
import "../stylesheets/homepage.css";
import { FaUserGroup } from "react-icons/fa6";
import { FaRegFileLines } from "react-icons/fa6";
import { FaRegClock } from "react-icons/fa6";
import { IoLanguage } from "react-icons/io5";

function Homepage() {
    return (
        <section className="homepage">
            <h1 className="invisible">Page d'accueil de Wordsmith Hub</h1>
            <Header />
            <div className="homepage_presentation_container">
                <div className="homepage_presentation_text_container">
                    <h2 className="homepage_presentation_title">Simplifiez la gestion de votre activité de traduction</h2>
                    <p className="homepage_presentation_text">Wordsmith Hub est un service complet destiné aux traducteurs indépendants. Gérez vos clients, suivez vos projets et créez vos factures au même endroit.</p>
                </div>
                <div className="homepage_image_container">
                    <img className="homepage_horizontal_image" src="../src/assets/image_01.avif" alt="Image d'illustration" />
                </div>
            </div>

            <div className="homepage_tools_container">
                <h2 className="homepage_tools_title">Tout ce qu'il vous faut pour réussir</h2>
                <p className="homepage_tools_text">Des outils pratiques conçus pour les traducteurs indépendants</p>
                <div className="homepage_vignette_container">
                    <HomepageVignette title="Gestion des clients" text="Enregistrez vos clients, leurs informations de contact et vos tarifs pour chaque paire de langue et vos différents services." icon={<FaUserGroup size={32} color="var(--color-blue-deep)" />} />
                    <HomepageVignette title="Suivi des projets" text="Organisez vos projets de traduction en notant leur statut, les langues de travail, le client final, etc." icon={<FaRegFileLines size={32} color="var(--color-blue-deep)" />} />
                    <HomepageVignette title="Gestion des commandes" text="Suivez votre charge de travail grâce aux informations détaillées sur chaque commande&nbsp;: compte de mots, tarifs, date de livraison, etc." icon={<FaRegClock size={32} color="var(--color-blue-deep)" />} />
                    <HomepageVignette title="Prise en charge de plusieurs langues" text="Gérez facilement toutes les paires de langues. Configurez facilement vos tarifs pour toutes les combinaisons et tous les services." icon={<IoLanguage size={32} color="var(--color-blue-deep)" />} />
                </div>
            </div>

            <div className="homepage_feature_container">
                <div className="homepage_image_container">
                    <img className="homepage_vertical_image" src="../src/assets/image_02.avif" alt="Image d'illustration" />
                </div>
                <div>
                    <h2 className="homepage_feature_title">Concentrez-vous sur la traduction, pas sur la gestion administrative</h2>
                    <HomepageFeature title="Gagnez du temps" text="Réduisez votre charge administrative grâce à l’automatisation." />
                    <HomepageFeature title="Faites-vous payer plus vite" text="Envoyez des factures professionnelles sitôt votre tâche achevée." />
                    <HomepageFeature title="Développez votre activité" text="Prenez des décisions réfléchies grâce aux informations sur vos activités les plus rentables." />
                    <HomepageFeature title="Optimisez votre organisation" text="Ne manquez jamais une échéance grâce à un système de gestion centralisée de vos commandes." />
                </div>
            </div>
            <Footer />
        </section>
    );
}

export default Homepage;