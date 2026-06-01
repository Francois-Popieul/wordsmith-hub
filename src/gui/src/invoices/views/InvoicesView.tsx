import { PlusSignIcon } from "../../assets/icons/icons";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import AppLayout from "../../components/ui/AppLayout";
import { useNavigate } from "react-router";
import { useApiClient } from "../../hooks/useApiClient";

function Invoices() {
    const { token } = useApiClient();
    const navigate = useNavigate();

    if (!token) {
        navigate("/");
        return null;
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