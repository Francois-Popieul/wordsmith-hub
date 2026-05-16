import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { createApiClient } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import ListContainer from "../../components/ui/ListContainer";
import Button from "../../components/ui/Button";
import { OrdersIcon, PlusSignIcon } from "../../assets/icons/icons";
import type LegalStatusDto from "../models/LegalStatusDto";
import AddLegalStatusModal from "./AddLegalStatusModal";

function LegalStatusListContainer() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);

    const [legalStatuses, setLegalStatuses] = useState<LegalStatusDto[]>([]);
    const { addToast } = useToast();
    const [isAddLegalStatusModalVisible, setIsAddLegalStatusModalVisible] = useState(false);

    useEffect(() => {
        const fetchLegalStatuses = async () => {
            try {
                const response = await apiClient.GetAllLegalStatusesEndpoint();
                setLegalStatuses(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    addToast("error", `Erreur de l'API : ${error.response.data}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement de la liste des statuts juridiques.", "top_right", 3000);
                }
            }
        };
        fetchLegalStatuses();
    }, [apiClient, addToast]);

    async function handleAddLegalStatus() {
        setIsAddLegalStatusModalVisible(true);
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
                <div className="legal_status_list">
                    {/* Render legal statuses here */}
                </div>
            </ListContainer>
            <AddLegalStatusModal isVisible={isAddLegalStatusModalVisible} onClose={() => setIsAddLegalStatusModalVisible(false)} />
        </>
    );
}

export default LegalStatusListContainer;
