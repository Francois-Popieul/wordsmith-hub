import Footer from "../../components/partials/Footer";
import Header from "../../components/partials/Header";
import Accordion from "../components/Accordion";
import "./Faq.css";

function Faq() {
    return (
        <>
            <Header />

            <div className="faq">

                <h1 className="faq_title">FAQ</h1>

                <Accordion title="Qu’est-ce que Wordsmith Hub&nbsp;?">
                    <p><i>Wordsmith Hub</i> est la plateforme tout‑en‑un pensée pour les traducteurs indépendants qui veulent gagner du temps et travailler plus sereinement. Elle centralise vos clients, vos projets, vos commandes, vos tarifs et votre facturation dans un espace simple, moderne et intuitif. L’objectif : vous permettre de vous concentrer sur votre métier, tout en automatisant les tâches administratives qui vous ralentissent.</p>
                </Accordion>

                <Accordion title="Comment ajouter un nouveau client&nbsp;?">
                    <p>Depuis votre tableau de bord, accédez à la section « Clients » puis cliquez sur « Ajouter un client ». Renseignez ses informations de contact, ses langues cibles, ses préférences éventuelles et ses conditions tarifaires. En quelques secondes, votre client est prêt à être utilisé pour créer des devis, projets et commandes.</p>
                </Accordion>

                <Accordion title="Puis-je gérer mes tarifs et mes devis&nbsp;?">
                    <p>Absolument. <i>Wordsmith Hub</i> vous permet de définir des tarifs flexibles (par mot, heure, page, forfait, etc.) et de créer des devis professionnels en un instant. Lors de la création d’un projet ou d’une commande, vos tarifs sont automatiquement appliqués, ce qui garantit cohérence, rapidité et zéro erreur.</p>
                </Accordion>

                <Accordion title="Comment suivre mes projets et commandes&nbsp;?">
                    <p>Votre tableau de bord vous offre une vision claire et actualisée de votre activité. Suivez l’avancement de vos projets, consultez les documents associés, vérifiez les échéances et visualisez les montants prévus ou facturés. <i>Wordsmith Hub</i> met en avant les tâches prioritaires pour vous aider à rester organisé et à respecter vos deadlines.</p>
                </Accordion>

                <Accordion title="Quelles fonctionnalités de facturation sont disponibles&nbsp;?">
                    <p><i>Wordsmith Hub</i> génère automatiquement vos factures à partir des commandes validées, en respectant vos paramètres (numérotation, mentions légales, TVA, etc.). Vous pouvez suivre les paiements, relancer les clients en retard et conserver un historique complet de vos transactions. L’objectif : simplifier votre gestion administrative et vous faire gagner un temps précieux.</p>
                </Accordion>

                <Accordion title="Mes données sont‑elles en sécurité&nbsp;?">
                    <p>Oui. <i>Wordsmith Hub</i> applique des standards de sécurité modernes pour protéger vos données. Toutes les communications sont chiffrées (HTTPS/TLS), et les accès sont strictement contrôlés. Nous mettons en place des mesures de sécurité proactives pour garantir la confidentialité et l’intégrité de vos informations professionnelles.</p>
                </Accordion>

                <Accordion title="Où mes données sont‑elles stockées&nbsp;?">
                    <p>Vos données sont hébergées sur des serveurs situés en Europe, conformes aux normes de sécurité et de disponibilité professionnelles. L’infrastructure est surveillée en continu afin d’assurer performance, stabilité et protection contre les incidents.</p>
                </Accordion>

                <Accordion title="Wordsmith Hub est‑il conforme au RGPD&nbsp;?">
                    <p>Oui. <i>Wordsmith Hub</i> respecte pleinement le RGPD. Vous gardez le contrôle sur vos données, pouvez demander leur export ou leur suppression, et nous ne partageons jamais vos informations avec des tiers sans votre consentement explicite.</p>
                </Accordion>

                <Accordion title="Puis‑je exporter mes données&nbsp;?">
                    <p>Oui. Vous pouvez exporter vos clients, projets, commandes et factures à tout moment. <i>Wordsmith Hub</i> vous garantit une totale portabilité de vos données pour que vous restiez libre et autonome.</p>
                </Accordion>

                <Accordion title="Proposez‑vous une sauvegarde automatique&nbsp;?">
                    <p>Oui. Vos données sont sauvegardées régulièrement afin de prévenir toute perte accidentelle. En cas d’incident, une restauration est possible pour garantir la continuité de votre activité.</p>
                </Accordion>

                <Accordion title="Comment obtenir de l’aide en cas de problème&nbsp;?">
                    <p>Vous pouvez contacter notre support directement depuis votre espace <i>Wordsmith Hub</i>. Nous répondons rapidement pour vous accompagner, résoudre vos problèmes et vous aider à tirer le meilleur parti de la plateforme.</p>
                </Accordion>

                <Accordion title="Wordsmith Hub évolue‑t‑il régulièrement&nbsp;?">
                    <p>Oui. Nous améliorons continuellement la plateforme en fonction des retours des utilisateurs. De nouvelles fonctionnalités, optimisations et intégrations sont ajoutées régulièrement pour vous offrir une expérience toujours plus fluide et performante.</p>
                </Accordion>

            </div>

            <Footer />
        </>
    );
}

export default Faq;