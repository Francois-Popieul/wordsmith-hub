import Footer from "../components/partials/Footer";
import Header from "../components/partials/Header";
import "../stylesheets/homepage.css";

function Homepage() {
    return (
        <section className="homepage">
            <h1 className="invisible">Page d'accueil de Wordsmith Hub</h1>
            <Header />
            <div className="homepage_presentation_container">
                <div>
                    <h2 className="homepage_presentation_title">Simplifiez la gestion de votre activité de traduction</h2>
                    <p className="homepage_presentation_text">Wordsmith Hub est un service complet destiné aux traducteurs indépendants. Gérez vos clients, suivez vos projets et créez vos factures au même endroit.</p>
                </div>
                <div className="homepage_image_container">
                    <img className="homepage_image" src="../src/assets/image_01.avif" alt="Image d'illustration" />
                </div>
            </div>

            <div className="homepage_tools_container">
                <h2>Tout ce qu'il vous faut pour réussir</h2>
                <p>Des outils pratiques conçus pour les traducteurs indépendants</p>
                <div className="homepage_vignette_container">

                </div>
            </div>

            <div className="homepage_feature_container">
                <div className="homepage_image_container">
                    <img className="homepage_image" src="../src/assets/image_02.avif" alt="Image d'illustration" />
                </div>
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
            <Footer />
        </section>
    );
}

export default Homepage;