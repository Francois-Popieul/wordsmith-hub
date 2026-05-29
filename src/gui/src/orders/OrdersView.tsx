import { PlusSignIcon } from "../assets/icons/icons";
import AppLayout from "../components/ui/AppLayout";
import PageHeader from "../components/ui/PageHeader";
import Button from "../components/ui/Button";
import { useNavigate } from "react-router";

function OrdersView() {
    const token = localStorage.getItem("wshToken");
    const navigate = useNavigate();

    if (!token) {
        navigate("/");
        return null;
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