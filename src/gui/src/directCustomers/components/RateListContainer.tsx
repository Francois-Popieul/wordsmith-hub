import { useEffect, useMemo, useState } from "react";
import { createApiClient, schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import ListContainer from "../../components/ui/ListContainer";
import Button from "../../components/ui/Button";
import { InvoicesIcon, PlusSignIcon } from "../../assets/icons/icons";
import * as zod from "zod";
import ConfirmationModal from "../../components/ui/ConfirmationModal";
import RateDataTable from "./RateDataTable";
import AddRateModal from "./AddRateModal";
import ProfileDto from "../../profile/models/ProfileDto";
import axios from "axios";
import type { Service } from "../../types/Service";
import type { TranslationLanguage } from "../../types/TranslationLanguage";

interface RateListContainerProps {
    directCustomerId: string;
}

function RateListContainer({ directCustomerId }: RateListContainerProps) {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const { addToast } = useToast();
    const [rates, setRates] = useState<zod.infer<typeof schemas.RateDto>[]>([]);
    const [rateToDeleteId, setRateToDeleteId] = useState<string | null>(null);
    const [isAddRateModalVisible, setIsAddRateModalVisible] = useState(false);
    // const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);
    // const [rateToUpdate, setRateToUpdate] = useState<zod.infer<typeof schemas.RateDto>> | null>(null);
    const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);
    const [profileData, setProfileData] = useState<ProfileDto | null>(null);
    const [services, setServices] = useState<Service[]>([]);
    const [languages, setLanguages] = useState<TranslationLanguage[]>([]);

    useEffect(() => {
        if (!token) return;
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                const profileData = new ProfileDto(response.id, response.firstName, response.lastName, response.email, response.phone, response.address, response.statusId, response.sourceLanguages, response.targetLanguages, response.services);
                setProfileData(profileData);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement des données de profil.", "top_right", 3000);
                }
            }
        };
        fetchProfileData();
    }, [apiClient, addToast, token]);

    useEffect(() => {
        if (!token) return;
        const fetchServices = async () => {
            try {
                const response = await apiClient.GetAllServicesEndpoint();
                setServices(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement des services.", "top_right", 3000);
                }
            }
        };
        fetchServices();
    }, [apiClient, addToast, token]);

    useEffect(() => {
        if (!token) return;
        const fetchLanguages = async () => {
            try {
                const response = await apiClient.GetAllLanguagesEndpoint();
                setLanguages(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement des langues.", "top_right", 3000);
                }
            }
        };
        fetchLanguages();
    }, [apiClient, addToast, token]);

    useEffect(() => {
        if (!token) return;
        const fetchRates = async () => {
            try {
                const response = await apiClient.GetAllRatesEndpoint({ pathParams: { directCustomerId } });
                setRates(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement des tarifs.", "top_right", 3000);
                }
            }
        };
        fetchRates();
    }, [apiClient, addToast, token, directCustomerId, refreshKey]);

    function handleAddRate() {
        setIsAddRateModalVisible(true);
    }

    function handleDelete(id: string) {
        setIsDeleteModalVisible(true);
        setRateToDeleteId(id);
        addToast("information", `Supprimer le tarif avec l'ID ${id}`, "top_right", 3000);
    }

    async function handleConfirmDelete() {
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
                addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s'est produite lors de la suppression du tarif.", "top_right", 3000);
            }
            return;
        }
        setIsDeleteModalVisible(false);
        setRateToDeleteId(null);
        addToast("success", "Tarif supprimé !", "top_right", 3000);
    }

    function handleCancelDelete() {
        setIsDeleteModalVisible(false);
        setRateToDeleteId(null);
        addToast("information", "Suppression du tarif annulée.", "top_right", 3000);
    }

    function handleEdit(id: string) {
        addToast("information", `Modifier le tarif avec l'ID ${id}`, "top_right", 3000);
    }

    return <>
        <ListContainer
            icon={<InvoicesIcon />}
            title="Tarifs des services"
            presentation="Tarifs acceptés pour les différents services"
            add_button_name="Ajouter un tarif"
            no_content_message="Aucun tarif enregistré pour le moment. Cliquez sur le bouton ci-dessous afin de définir vos tarifs pour les services que vous proposez."
            no_content_button={<Button name="Ajouter votre premier tarif" variant="light" width="default" type="button" onClick={handleAddRate}><PlusSignIcon /></Button>}
            list_length={rates.length}
            onClickAdd={handleAddRate}
        >
            <RateDataTable rates={rates} services={services} languages={languages} onEdit={handleEdit} onDelete={handleDelete} />
        </ListContainer>
        <AddRateModal directCustomerId={directCustomerId} freelanceProfile={profileData} isVisible={isAddRateModalVisible} onClose={() => setIsAddRateModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
        {/* <UpdateRateModal isVisible={isUpdateModalVisible} rate={rateToUpdate} onClose={() => setIsUpdateModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} /> */}
        <ConfirmationModal isVisible={isDeleteModalVisible} title="Supprimer le tarif" message="Voulez-vous vraiment supprimer ce tarif ?" onConfirm={handleConfirmDelete} onCancel={handleCancelDelete} />
    </>
}

export default RateListContainer;