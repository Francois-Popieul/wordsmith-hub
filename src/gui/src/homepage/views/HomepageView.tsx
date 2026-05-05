import Footer from "../../components/partials/Footer";
import Header from "../../components/partials/Header";
import HomepageFeature from "../components/HomepageFeature";
import HomepageVignette from "../components/HomepageVignette";
import "../../stylesheets/homepage.css";
import { Customers } from "../../assets/icons/icons";
import { Projects } from "../../assets/icons/icons";
import { ClockIcon } from "../../assets/icons/icons";
import { LanguageIcon } from "../../assets/icons/icons";

function HomepageView() {
    return (<>
        <Header />
        <main className="homepage">
            <h1 className="invisible">Page d’accueil de Wordsmith Hub</h1>
            <section className="homepage_presentation_container">
                <div className="homepage_presentation_text_container">
                    <h2 className="homepage_presentation_title">Simplifiez la gestion de votre activité de traduction</h2>
                    <p className="homepage_presentation_text">Wordsmith Hub est un service complet destiné aux traducteurs indépendants. Gérez vos clients, suivez vos projets et créez vos factures au même endroit.</p>
                </div>
                <div className="homepage_image_container">
                    <img className="homepage_horizontal_image" src="../src/assets/image_01.avif" alt="Image d’illustration représentant un chat sur un clavier" />
                </div>
            </section>

            <section className="homepage_tools_container">
                <h2 className="homepage_tools_title">Tout ce qu’il vous faut pour réussir</h2>
                <p className="homepage_tools_text">Des outils pratiques conçus pour les traducteurs indépendants</p>
                <div className="homepage_vignette_container">
                    <HomepageVignette title="Gestion des clients" text="Enregistrez vos clients, leurs informations de contact et vos tarifs pour chaque paire de langue et vos différents services." icon={<Customers size={32} color="var(--color-blue-deep)" />} />
                    <HomepageVignette title="Suivi des projets" text="Organisez vos projets de traduction en notant leur statut, les langues de travail, le client final, etc." icon={<Projects size={32} color="var(--color-blue-deep)" />} />
                    <HomepageVignette title="Gestion des commandes" text="Suivez votre charge de travail grâce aux informations détaillées sur chaque commande&nbsp;: compte de mots, tarifs, date de livraison, etc." icon={<ClockIcon size={32} color="var(--color-blue-deep)" />} />
                    <HomepageVignette title="Prise en charge de plusieurs langues" text="Gérez facilement toutes les paires de langues. Configurez facilement vos tarifs pour toutes les combinaisons et tous les services." icon={<LanguageIcon size={32} color="var(--color-blue-deep)" />} />
                </div>
            </section>

            <section className="homepage_feature_container">
                <div className="homepage_image_container">
                    <img className="homepage_vertical_image" src="../src/assets/image_02.avif" alt="Image d’illustration représentant une bibliothèque" />
                </div>
                <div>
                    <h2 className="homepage_feature_title">Concentrez-vous sur la traduction, pas sur la gestion administrative</h2>
                    <HomepageFeature title="Gagnez du temps" text="Réduisez votre charge administrative grâce à l’automatisation." />
                    <HomepageFeature title="Faites-vous payer plus vite" text="Envoyez des factures professionnelles sitôt votre tâche achevée." />
                    <HomepageFeature title="Développez votre activité" text="Prenez des décisions réfléchies grâce aux informations sur vos activités les plus rentables." />
                    <HomepageFeature title="Optimisez votre organisation" text="Ne manquez jamais une échéance grâce à un système de gestion centralisée de vos commandes." />
                </div>
            </section>
        </main>
        <Footer />
    </>
    );
}

export default HomepageView;