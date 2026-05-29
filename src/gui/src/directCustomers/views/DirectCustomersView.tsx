import AppLayout from "../../components/ui/AppLayout";
import Button from "../../components/ui/Button";
import PageHeader from "../../components/ui/PageHeader";
import { PlusSignIcon } from "../../assets/icons/icons";
import { useEffect, useState } from "react";
import AddDirectCustomerModal from "../components/AddDirectCustomerModal";
import { useNavigate } from "react-router";
import DirectCustomerDataTable from "../components/DirectCustomerDataTable";
import { useToast } from "../../hooks/useToast";
import { schemas } from "../../infrastructure/openApi/client";
import axios from "axios";
import * as zod from "zod";
import UpdateDirectCustomerModal from "../components/UpdateDirectCustomerModal";
import ConfirmationModal from "../../components/ui/ConfirmationModal";
import { useApiClient } from "../../hooks/useApiClient";

function DirectCustomers() {
    const { token, apiClient } = useApiClient();
    const navigate = useNavigate();
    const [isAddModalVisible, setIsAddModalVisible] = useState(false);
    const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);
    const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
    const [directCustomers, setDirectCustomers] = useState<zod.infer<typeof schemas.DirectCustomerDto>[]>([]);
    const [customers, setCustomers] = useState<zod.infer<typeof schemas.DirectCustomerDto>[]>([]);
    const [customerToUpdate, setCustomerToUpdate] = useState<zod.infer<typeof schemas.DirectCustomerDto> | null>(null);
    const [customerToDeleteId, setCustomerToDeleteId] = useState<string | null>(null);
    const [refreshKey, setRefreshKey] = useState(0);
    const { addToast } = useToast();

    useEffect(() => {
        if (!token) return;
        const fetchDirectCustomers = async () => {
            try {
                const response = await apiClient.GetAllDirectCustomersEndpoint();
                setDirectCustomers(response);
                setCustomers(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des clients directs.", "top_right", 3000);
                }
            }
        };
        fetchDirectCustomers();
    }, [apiClient, addToast, token, refreshKey]);

    if (!token) {
        navigate("/");
        return null;
    }

    function handleUpdate(id: string) {
        const customer = directCustomers.find(c => c.id === id) || null;
        setCustomerToUpdate(customer);
        setIsUpdateModalVisible(true);
    }

    function handleDelete(id: string) {
        setCustomerToDeleteId(id);
        setIsDeleteModalVisible(true);
    }

    async function handleConfirmDelete() {
        if (customerToDeleteId) {
            try {
                await apiClient.DeleteDirectCustomerEndpoint({
                    pathParams: { directCustomerId: customerToDeleteId },
                });
                setDirectCustomers(prev => prev.filter(c => c.id !== customerToDeleteId));
                setCustomers(prev => prev.filter(c => c.id !== customerToDeleteId));
                addToast("success", "Client direct supprimé !", "top_right", 3000);
            } catch (error) {
                if (error instanceof zod.ZodError) {
                    // 204 No Content: HTTP succeeded but the auto-generated schema can't parse an empty body
                    setDirectCustomers(prev => prev.filter(c => c.id !== customerToDeleteId));
                    setCustomers(prev => prev.filter(c => c.id !== customerToDeleteId));
                    addToast("success", "Client direct supprimé !", "top_right", 3000);
                } else if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors de la suppression du client direct.", "top_right", 3000);
                }
            }
        }
        setCustomerToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    function handleCancelDelete() {
        setCustomerToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    function handleView(id: string) {
        navigate(`/direct-customer/${id}`);
        console.log("View customer with id:", id);
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Clients" pageSubtitle="Gérez vos clients et vos tarifs" button={<Button variant="blue" name="Ajouter un client" width="default" type="button" onClick={() => setIsAddModalVisible(true)}><PlusSignIcon /></Button>}></PageHeader>
                <DirectCustomerDataTable
                    directCustomers={customers}
                    onAdd={() => setIsAddModalVisible(true)}
                    onView={(id) => handleView(id)}
                    onEdit={(id) => handleUpdate(id)}
                    onDelete={(id) => handleDelete(id)}
                />
                <AddDirectCustomerModal isVisible={isAddModalVisible} onClose={() => setIsAddModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
                <UpdateDirectCustomerModal customer={customerToUpdate} isVisible={isUpdateModalVisible} onClose={() => setIsUpdateModalVisible(false)} />
                <ConfirmationModal isVisible={isDeleteModalVisible} title="Supprimer le client" message="Voulez-vous vraiment supprimer ce client ?" onConfirm={handleConfirmDelete} onCancel={handleCancelDelete} />
            </AppLayout>
        </>
    );
}

export default DirectCustomers;
