import Footer from "../../components/partials/Footer";
import Header from "../../components/partials/Header";
import "../../stylesheets/legal_conditions.css";

function PrivacyPolicy() {
    return (
        <>
            <Header />
            <main className="privacy_policy">
                <h1>Politique de confidentialité</h1>

                <p>Dernière mise à jour&nbsp;: 1<sup>er</sup>&nbsp;mai&nbsp;2026</p>

                <p>Chez WordsmithHub, nous nous engageons à protéger votre vie privée. Cette politique de confidentialité explique comment nous collectons, utilisons et protégeons vos informations personnelles lorsque vous utilisez notre plateforme.</p>

                <p className="back_to_top"><a href="#top">Revenir en haut</a></p>

                <h2>Collecte d’informations</h2>

                <p>Nous collectons des informations personnelles telles que votre nom, adresse e-mail, et autres données nécessaires pour créer et gérer votre compte. Nous pouvons également collecter des informations sur votre utilisation de la plateforme pour améliorer nos services.</p>

                <p className="back_to_top"><a href="#top">Revenir en haut</a></p>

                <h2>Utilisation des informations</h2>

                <p>Nous utilisons vos informations personnelles pour fournir et améliorer nos services, communiquer avec vous, et personnaliser votre expérience sur notre plateforme. Nous ne vendons ni ne partageons vos informations avec des tiers à des fins commerciales.</p>

                <p className="back_to_top"><a href="#top">Revenir en haut</a></p>

                <h2>Protection des informations</h2>

                <p>Nous mettons en place des mesures de sécurité pour protéger vos informations personnelles contre l’accès non autorisé, la divulgation, ou la destruction. Cependant, aucune méthode de transmission sur Internet ou de stockage électronique n’est totalement sécurisée.</p>

                <p className="back_to_top"><a href="#top">Revenir en haut</a></p>

                <h2>Vos droits</h2>

                <p>Vous avez le droit d’accéder, de corriger ou de supprimer vos informations personnelles. Vous pouvez également vous opposer à l’utilisation de vos informations à des fins de marketing. Pour exercer ces droits, veuillez nous contacter à l’adresse e-mail fournie sur notre site.</p>

                <p className="back_to_top"><a href="#top">Revenir en haut</a></p>

                <h2>Modifications de la politique de confidentialité</h2>

                <p>Nous nous réservons le droit de modifier cette politique de confidentialité à tout moment. Nous vous informerons de tout changement en publiant la nouvelle politique sur notre site. Veuillez consulter régulièrement cette page pour rester informé de nos pratiques en matière de confidentialité.</p>

                <p className="back_to_top"><a href="#top">Revenir en haut</a></p>
            </main>
            <Footer />
        </>
    );
}

export default PrivacyPolicy;