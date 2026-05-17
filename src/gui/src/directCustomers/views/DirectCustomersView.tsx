import AppLayout from "../../components/ui/AppLayout";
import Button from "../../components/ui/Button";
import PageHeader from "../../components/ui/PageHeader";
import { PlusSignIcon } from "../../assets/icons/icons";
import { useState } from "react";
import AddDirectCustomerModal from "../components/AddDirectCustomerModal";
import { useNavigate } from "react-router";

function DirectCustomers() {
    const navigate = useNavigate();
    const [isAddModalVisible, setIsAddModalVisible] = useState(false);
    const token = localStorage.getItem("wshToken");

    if (!token) {
        navigate("/");
        return null;
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Clients" pageSubtitle="Gérez vos clients et vos tarifs" button={<Button variant="blue" name="Ajouter un client" width="default" type="button" onClick={() => setIsAddModalVisible(true)}><PlusSignIcon /></Button>}></PageHeader>
                <AddDirectCustomerModal isVisible={isAddModalVisible} onClose={() => setIsAddModalVisible(false)} />
            </AppLayout>
        </>
    );
}

export default DirectCustomers;