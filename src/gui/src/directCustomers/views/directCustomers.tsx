import AppLayout from "../../components/ui/AppLayout";
import Button from "../../components/ui/Button";
import PageHeader from "../../components/ui/PageHeader";
import { PlusSign } from "../../assets/icons/icons";

function DirectCustomers() {
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Clients" pageSubtitle="Gérez vos clients et vos tarifs" button={<Button variant="dark" name="Ajouter un client" width="default" type="button"><PlusSign /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default DirectCustomers;