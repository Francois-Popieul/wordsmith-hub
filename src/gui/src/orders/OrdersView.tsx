import { FaPlus } from "react-icons/fa6";
import AppLayout from "../components/ui/AppLayout";
import PageHeader from "../components/ui/PageHeader";
import Button from "../components/ui/Button";

function OrdersView() {
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Commandes" pageSubtitle="Suivez vos commandes" button={<Button variant="dark" name="Ajouter une commande" width="default" type="button"><FaPlus /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default OrdersView;