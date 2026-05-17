import { PlusSignIcon } from "../assets/icons/icons";
import AppLayout from "../components/ui/AppLayout";
import PageHeader from "../components/ui/PageHeader";
import Button from "../components/ui/Button";
import { Navigate } from "react-router";

function OrdersView() {
    const token = localStorage.getItem("wshToken");

    if (!token) {
        return <Navigate to="/" />;
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Commandes" pageSubtitle="Suivez vos commandes" button={<Button variant="blue" name="Ajouter une commande" width="default" type="button"><PlusSignIcon /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default OrdersView;