import { PlusSign } from "../../assets/icons/icons";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import AppLayout from "../../components/ui/AppLayout";

function Invoices() {
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Factures" pageSubtitle="Créez et gérez vos factures pour les commandes terminées" button={<Button variant="dark" name="Créer une facture" width="default" type="button"><PlusSign /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default Invoices;