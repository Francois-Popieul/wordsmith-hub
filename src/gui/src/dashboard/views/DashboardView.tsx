import "../components/Card.css";
import "../components/QuickActionCard.css";
import { useNavigate } from "react-router";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import { useEffect, useState } from "react";
import ProfileDto from "../../profile/models/ProfileDto";
import axios from "axios";
import { useToast } from "../../hooks/useToast";
import Card from "../components/Card";
import { CustomersIcon, InvoicesIcon, OrdersIcon, ProjectsIcon } from "../../assets/icons/icons";
import QuickActionCard from "../components/QuickActionCard";
import QuickActionContainer from "../components/QuickActionContainer";
import AddDirectCustomerModal from "../../directCustomers/components/AddDirectCustomerModal";
import AddProjectModal from "../../projects/components/AddProjectModal";
import { useApiClient } from "../../hooks/useApiClient";
import { useDirectCustomerCount, useProjectCount } from "../../hooks/useStats";

function DashboardView() {
    const { token, apiClient } = useApiClient();
    const navigate = useNavigate();
    const { addToast } = useToast();
    const [profileData, setProfileData] = useState<ProfileDto | void>();
    const [loading, setLoading] = useState(true);
    const [orderNumber, setOrderNumber] = useState(0);
    const [earnings, setEarnings] = useState(0);
    const [isAddCustomerModalVisible, setIsAddCustomerModalVisible] = useState(false);
    const [isAddProjectModalVisible, setIsAddProjectModalVisible] = useState(false);
    const directCustomerCount = useDirectCustomerCount();
    const projectCount = useProjectCount();

    useEffect(() => {
        if (!token) return;
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                const profileData = new ProfileDto(response.id, response.firstName, response.lastName, response.email, response.phone, response.address, response.statusId, response.sourceLanguages, response.targetLanguages, response.services);
                setProfileData(profileData);
                setOrderNumber(0);
                setEarnings(0);
                setLoading(false);
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

    function handleAddProject() {
        if (directCustomerCount === 0) {
            addToast("information", "Ajoutez un client direct pour pouvoir créer un projet.", "top_right", 3000);
            return;
        }
        setIsAddProjectModalVisible(true);
    }

    if (!token) {
        navigate("/");
        return null;
    }

    return !loading ? (
        <AppLayout>
            <PageHeader pageTitle="Tableau de bord" pageSubtitle={profileData?.firstName ? `Bienvenue, ${profileData.firstName}\u00A0! Retrouvez ici un résumé de votre activité.` : "Bienvenue\u00A0! Retrouvez ici un résumé de votre activité."}></PageHeader>
            <div className="card_container">
                <Card title="Total de clients" icon={<CustomersIcon />} value={directCustomerCount.toString()} statistics={directCustomerCount <= 1 ? "Collaboration en cours" : "Collaborations en cours"} />
                <Card title="Projets actifs" icon={<ProjectsIcon />} value={projectCount.toString()} statistics={projectCount <= 1 ? "Projet en cours" : "Projets en cours"} />
                <Card title="Commandes en attente" icon={<OrdersIcon />} value="0" statistics={orderNumber > 0 ? "Commandes non terminées" : "Commande en attente"} />
                <Card title="Total des revenus" icon={<InvoicesIcon />} value="0" statistics={earnings > 0 ? "Revenus perçus" : "Aucun revenu"} />
            </div>
            <QuickActionContainer
                title="Actions rapides"
                description="Gérer rapidement vos clients, projets et factures"
            >
                <QuickActionCard
                    icon={<CustomersIcon />}
                    title="Ajouter un client"
                    description="Ajouter un client à votre liste de contacts"
                    onClick={() => setIsAddCustomerModalVisible(true)}
                />
                <QuickActionCard
                    icon={<ProjectsIcon />}
                    title="Créer un projet"
                    description="Créer un nouveau projet pour un client"
                    onClick={handleAddProject}
                />
                <QuickActionCard
                    icon={<InvoicesIcon />}
                    title="Créer une facture"
                    description="Créer une nouvelle facture pour un client"
                    onClick={() => { /* setIsAddInvoiceModalVisible(true) */ }}
                />
            </QuickActionContainer>
            <AddDirectCustomerModal isVisible={isAddCustomerModalVisible} onClose={() => setIsAddCustomerModalVisible(false)} />
            <AddProjectModal isVisible={isAddProjectModalVisible} onClose={() => setIsAddProjectModalVisible(false)} />
        </AppLayout>
    ) : <AppLayout>
        <p>Chargement des données en cours…</p>
    </AppLayout>;
}

export default DashboardView;
