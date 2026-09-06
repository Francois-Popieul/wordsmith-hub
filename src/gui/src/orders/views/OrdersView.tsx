import { PlusSignIcon } from "../../assets/icons/icons";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import OrderDataTable from "../components/OrderDataTable";
import { useEffect, useState } from "react";
import { useApiClient } from "../../hooks/useApiClient";
import type { schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";
import { useStaticTables } from "../../hooks/useStaticTables/useStaticTables";
import axios from "axios";
import { useToast } from "../../hooks/useToast/useToast";

function OrdersView() {
    const { apiClient } = useApiClient();
    const { addToast } = useToast();
    const [workOrders, setWorkOrders] = useState<zod.infer<typeof schemas.WorkOrderDto>[]>([]);
    const workOrderStatuses = useStaticTables().workOrderStatuses;

    useEffect(() => {
        const fetchWorkOrders = async () => {
            try {
                const response = await apiClient.GetAllWorkOrdersEndpoint();
                setWorkOrders(response);
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

        fetchWorkOrders();
    }, [apiClient, addToast]);

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Commandes" pageSubtitle="Suivez vos commandes" button={<Button variant="blue" name="Ajouter une commande" width="default" type="button" onClick={() => { addToast("information", "Fonction en cours d'implémentation", "top_right", 3000); }}><PlusSignIcon /></Button>}></PageHeader>
                <OrderDataTable workOrders={workOrders} workOrderStatuses={workOrderStatuses} onAdd={() => { addToast("information", "Fonction en cours d'implémentation", "top_right", 3000); }} onEdit={() => { addToast("information", "Fonction en cours d'implémentation", "top_right", 3000); }} onStatusChange={() => { addToast("information", "Fonction en cours d'implémentation", "top_right", 3000); }} onDelete={() => { addToast("information", "Fonction en cours d'implémentation", "top_right", 3000); }} />
            </AppLayout>
        </>
    );
}

export default OrdersView;