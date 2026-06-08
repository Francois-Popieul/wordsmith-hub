import { PlusSignIcon } from "../../assets/icons/icons";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import AppLayout from "../../components/ui/AppLayout";
import { useToast } from "../../hooks/useToast/useToast";

function Invoices() {
    const { addToast } = useToast();
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Factures" pageSubtitle="Créez et gérez vos factures pour les commandes terminées" button={<Button variant="blue" name="Créer une facture" width="default" type="button" onClick={() => {
                    // TODO: implémenter la création de facture
                    addToast("information", "Fonctionnalité de création de facture en cours de développement.", "top_right", 3000);
                }}><PlusSignIcon /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default Invoices;