import { useEffect, useState } from "react";
import axios from "axios";
import { schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import ListContainer from "../../components/ui/ListContainer";
import Button from "../../components/ui/Button";
import { OrdersIcon, PlusSignIcon } from "../../assets/icons/icons";
import AddLegalStatusModal from "./AddLegalStatusModal";
import LegalStatusDataTable from "./LegalStatusDataTable";
import * as zod from "zod";
import { useApiClient } from "../../hooks/useApiClient";
import ConfirmationModal from "../../components/ui/ConfirmationModal";

function LegalStatusListContainer() {
    const { token, apiClient } = useApiClient();
    const [legalStatuses, setLegalStatuses] = useState<zod.infer<typeof schemas.LegalStatusDto>[]>([]);
    const { addToast } = useToast();
    const [isAddLegalStatusModalVisible, setIsAddLegalStatusModalVisible] = useState(false);
    const [isDeleteConfirmationModalVisible, setIsDeleteConfirmationModalVisible] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);
    const [legalStatusToDeleteId, setLegalStatusToDeleteId] = useState<string | null>(null);

    useEffect(() => {
        if (!token) return;
        const fetchLegalStatuses = async () => {
            try {
                const response = await apiClient.GetAllLegalStatusesEndpoint();
                setLegalStatuses(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des statuts juridiques.", "top_right", 3000);
                }
            }
        };
        fetchLegalStatuses();
    }, [apiClient, addToast, refreshKey, token]);

    async function handleAddLegalStatus() {
        setIsAddLegalStatusModalVisible(true);
    }

    async function handleEditLegalStatus(id: string) {
        addToast("information", `Modifier le statut juridique avec l’ID ${id}`, "top_right", 3000);
    }

    async function handleDeleteLegalStatus(id: string) {
        setLegalStatusToDeleteId(id);
        setIsDeleteConfirmationModalVisible(true);
    }

    async function confirmDeleteLegalStatus() {
        if (!legalStatusToDeleteId) return;
        try {
            await apiClient.DeleteLegalStatusEndpoint({ pathParams: { legalStatusId: legalStatusToDeleteId } });
            addToast("success", "Statut juridique supprimé.", "top_right", 3000);
            setRefreshKey(k => k + 1);
            setIsDeleteConfirmationModalVisible(false);
            setLegalStatusToDeleteId(null);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la suppression du statut juridique.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            <ListContainer
                icon={<OrdersIcon />}
                title="Statuts juridiques"
                presentation="Gérer vos statuts juridiques pour la facturation"
                add_button_name="Ajouter un statut"
                no_content_message="Aucun statut juridique enregistré pour le moment."
                no_content_button={<Button name="Ajouter votre premier statut" variant="light" width="default" type="button" onClick={handleAddLegalStatus}><PlusSignIcon /></Button>}
                list_length={legalStatuses.length}
                onClickAdd={handleAddLegalStatus}
            >
                <LegalStatusDataTable legalStatuses={legalStatuses} onEdit={(id) => handleEditLegalStatus(id)} onDelete={(id) => handleDeleteLegalStatus(id)} />
            </ListContainer>
            <AddLegalStatusModal isVisible={isAddLegalStatusModalVisible} onClose={() => setIsAddLegalStatusModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
            <ConfirmationModal title="Supprimer le statut juridique"
                message="Voulez-vous vraiment supprimer ce statut juridique ?"
                onConfirm={confirmDeleteLegalStatus} onCancel={() => setIsDeleteConfirmationModalVisible(false)} isVisible={isDeleteConfirmationModalVisible} />
        </>
    );
}

export default LegalStatusListContainer;
