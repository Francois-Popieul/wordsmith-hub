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

function DashboardView() {
    const { token, apiClient } = useApiClient();
    const navigate = useNavigate();
    const { addToast } = useToast();
    const [profileData, setProfileData] = useState<ProfileDto | void>();
    const [loading, setLoading] = useState(true);
    const [projectNumber, setProjectNumber] = useState(0);
    const [orderNumber, setOrderNumber] = useState(0);
    const [earnings, setEarnings] = useState(0);
    const [isAddCustomerModalVisible, setIsAddCustomerModalVisible] = useState(false);
    const [isAddProjectModalVisible, setIsAddProjectModalVisible] = useState(false);
    // const [isAddInvoiceModalVisible, setIsAddInvoiceModalVisible] = useState(false);

    useEffect(() => {
        if (!token) return;
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                const profileData = new ProfileDto(response.id, response.firstName, response.lastName, response.email, response.phone, response.address, response.statusId, response.sourceLanguages, response.targetLanguages, response.services);
                setProfileData(profileData);
                setProjectNumber(0);
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

    if (!token) {
        navigate("/");
        return null;
    }

    return !loading ? (
        <AppLayout>
            <PageHeader pageTitle="Tableau de bord" pageSubtitle={profileData?.firstName ? `Bienvenue, ${profileData.firstName}\u00A0! Retrouvez ici un résumé de votre activité.` : "Bienvenue\u00A0! Retrouvez ici un résumé de votre activité."}></PageHeader>
            <div className="card_container">
                <Card title="Total de clients" icon={<CustomersIcon />} value="0" statistics="Collaborations en cours" />
                <Card title="Projets actifs" icon={<ProjectsIcon />} value="0" statistics={projectNumber > 0 ? "Projets en cours" : "Projet en cours"} />
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
                    onClick={() => setIsAddProjectModalVisible(true)}
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
            {/* <AddInvoiceModal isVisible={isAddInvoiceModalVisible} onClose={() => setIsAddInvoiceModalVisible(false)} /> */}
        </AppLayout>
    ) : <p>Chargement des données en cours…</p>;
}

export default DashboardView;
