import { PlusSignIcon } from "../../assets/icons/icons";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import AppLayout from "../../components/ui/AppLayout";
import { Navigate } from "react-router";

function Invoices() {
    const token = localStorage.getItem("wshToken");

    if (!token) {
        return <Navigate to="/" />;
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Factures" pageSubtitle="Créez et gérez vos factures pour les commandes terminées" button={<Button variant="blue" name="Créer une facture" width="default" type="button"><PlusSignIcon /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default Invoices;