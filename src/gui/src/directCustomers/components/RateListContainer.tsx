import { useEffect, useState } from "react";
import { schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast/useToast";
import ListContainer from "../../components/ui/ListContainer";
import Button from "../../components/ui/Button";
import { InvoicesIcon, PlusSignIcon } from "../../assets/icons/icons";
import * as zod from "zod";
import ConfirmationModal from "../../components/ui/ConfirmationModal";
import RateDataTable from "./RateDataTable";
import AddRateModal from "./AddRateModal";
import axios from "axios";
import { useApiClient } from "../../hooks/useApiClient";
import UpdateRateModal from "./UpdateRateModal";
import { useStaticTables } from "../../hooks/useStaticTables/useStaticTables";

interface RateListContainerProps {
    directCustomerId: string;
    directCustomerCurrencySign: string;
}

function RateListContainer({ directCustomerId, directCustomerCurrencySign }: RateListContainerProps) {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [rates, setRates] = useState<zod.infer<typeof schemas.RateDto>[]>([]);
    const [rateToDeleteId, setRateToDeleteId] = useState<string | null>(null);
    const [isAddRateModalVisible, setIsAddRateModalVisible] = useState(false);
    const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);
    const [rateToUpdate, setRateToUpdate] = useState<zod.infer<typeof schemas.RateDto> | null>(null);
    const [isDeleteRateModalVisible, setIsDeleteRateModalVisible] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);
    const [profileData, setProfileData] = useState<zod.infer<typeof schemas.ProfileDto> | null>(null);
    const services = useStaticTables().services;
    const languages = useStaticTables().languages;

    useEffect(() => {
        if (!token) return;
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                const profileData: zod.infer<typeof schemas.ProfileDto> = response;
                setProfileData(profileData);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement des données de profil.", "top_right", 3000);
                }
            }
        };
        fetchProfileData();
    }, [apiClient, addToast, token]);



    useEffect(() => {
        if (!token) return;
        const fetchRates = async () => {
            try {
                const response = await apiClient.GetAllRatesByCustomerIdEndpoint({ pathParams: { directCustomerId } });
                setRates(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement des tarifs.", "top_right", 3000);
                }
            }
        };
        fetchRates();
    }, [apiClient, addToast, token, directCustomerId, refreshKey]);


    function handleAddRate() {
        if (!profileData || profileData.services?.length === 0 || profileData.sourceLanguages?.length === 0 || profileData.targetLanguages?.length === 0) {
            addToast("error", "Renseignez vos langues et services dans votre profil pour pouvoir ajouter des tarifs.", "top_right", 3000);
            return;
        }
        setIsAddRateModalVisible(true);
    }

    function handleDelete(id: string) {
        setRateToDeleteId(id);
        setIsDeleteRateModalVisible(true);
    }

    async function handleConfirmRateDelete() {
        if (!rateToDeleteId) {
            addToast("error", "Aucun tarif à supprimer.", "top_right", 3000);
            return;
        }
        try {
            await apiClient.DeleteRateEndpoint({ pathParams: { rateId: rateToDeleteId } });
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la suppression du tarif.", "top_right", 3000);
            }
            return;
        }
        setIsDeleteRateModalVisible(false);
        setRateToDeleteId(null);
        addToast("success", "Tarif supprimé !", "top_right", 3000);
        setRefreshKey(k => k + 1);
    }

    function handleCancelRateDelete() {
        setRateToDeleteId(null);
        setIsDeleteRateModalVisible(false);
    }

    function handleEdit(id: string) {
        const rate = rates.find(r => r.id === id);
        if (rate) {
            setRateToUpdate(rate);
            setIsUpdateModalVisible(true);
        } else {
            addToast("error", "Tarif introuvable.", "top_right", 3000);
        }
    }

    function handleCloseUpdateModal() {
        setIsUpdateModalVisible(false);
        setRateToUpdate(null);
    }

    return <>
        <ListContainer
            icon={<InvoicesIcon />}
            title="Tarifs des services"
            presentation="Tarifs acceptés pour les différents services"
            add_button_name="Ajouter un tarif"
            no_content_message="Aucun tarif enregistré pour le moment."
            no_content_button={<Button name="Ajouter votre premier tarif" variant="light" width="default" type="button" onClick={handleAddRate}><PlusSignIcon /></Button>}
            list_length={rates.length}
            onClickAdd={handleAddRate}
        >
            <RateDataTable rates={rates} directCustomerCurrencySign={directCustomerCurrencySign} services={services} languages={languages} onEdit={(id) => handleEdit(id)} onDelete={(id) => handleDelete(id)} />
        </ListContainer>
        <AddRateModal directCustomerId={directCustomerId} directCustomerCurrencySign={directCustomerCurrencySign} freelanceProfile={profileData} isVisible={isAddRateModalVisible} onClose={() => setIsAddRateModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
        {rateToUpdate && (
            <UpdateRateModal isVisible={isUpdateModalVisible} rate={rateToUpdate} directCustomerId={directCustomerId} directCustomerCurrencySign={directCustomerCurrencySign} freelanceProfile={profileData} onClose={handleCloseUpdateModal} onSuccess={() => setRefreshKey(k => k + 1)} />
        )}
        <ConfirmationModal isVisible={isDeleteRateModalVisible} title="Supprimer le tarif" message="Voulez-vous vraiment supprimer ce tarif ?" onConfirm={handleConfirmRateDelete} onCancel={handleCancelRateDelete} />
    </>
}

export default RateListContainer;