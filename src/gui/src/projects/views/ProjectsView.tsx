import { PlusSignIcon } from "../../assets/icons/icons";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import { useNavigate } from "react-router";
import AddProjectModal from "../components/AddProjectModal";
import { useEffect, useState } from "react";
import ProjectDataTable from "../components/ProjectDataTable";
import { schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast/useToast";
import * as zod from "zod";
import axios from "axios";
import ConfirmationModal from "../../components/ui/ConfirmationModal";
import { useApiClient } from "../../hooks/useApiClient";
import UpdateProjectModal from "../components/UpdateProjectModal";
import { useDirectCustomerCount } from "../../hooks/useStats";

function ProjectsView() {
    const { token, apiClient } = useApiClient();
    const navigate = useNavigate();
    const [refreshKey, setRefreshKey] = useState(0);
    const { addToast } = useToast();
    const directCustomerCount = useDirectCustomerCount();
    const [projects, setProjects] = useState<zod.infer<typeof schemas.ProjectDto>[]>([]);
    const [projectStatuses, setProjectStatuses] = useState<zod.infer<typeof schemas.Status>[]>([]);
    const [isAddModalVisible, setIsAddModalVisible] = useState(false);
    const [projectToUpdate, setProjectToUpdate] = useState<zod.infer<typeof schemas.ProjectDto> | null>(null);
    const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);
    const [projectToDeleteId, setProjectToDeleteId] = useState<string | null>(null);
    const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);

    useEffect(() => {
        if (!token) return;
        const fetchProjects = async () => {
            try {
                const response = await apiClient.GetAllProjectsEndpoint();
                setProjects(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des projets.", "top_right", 3000);
                }
            }
        };
        fetchProjects();
    }, [apiClient, addToast, token, refreshKey]);

    useEffect(() => {
        if (!token) return;
        const fetchProjectStatuses = async () => {
            try {
                const response = await apiClient.GetAllProjectStatusesEndpoint();
                setProjectStatuses(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des statuts de projets.", "top_right", 3000);
                }
            }
        };
        fetchProjectStatuses();
    }, [apiClient, addToast, token]);

    if (!token) {
        navigate("/");
        return null;
    }

    function handleUpdate(id: string) {
        const project = projects.find(p => p.id === id) || null;
        setProjectToUpdate(project);
        setIsUpdateModalVisible(true);
    }

    function handleConfirmUpdate() {
        setProjectToUpdate(null);
        setIsUpdateModalVisible(false);
        setRefreshKey(k => k + 1);
    }

    function handleDelete(id: string) {
        setProjectToDeleteId(id);
        setIsDeleteModalVisible(true);
    }

    async function handleConfirmDelete() {
        if (!token || !projectToDeleteId) return;
        try {
            await apiClient.DeleteProjectEndpoint({
                pathParams: { projectId: projectToDeleteId },
            });
            setProjects(prev => prev.filter(p => p.id !== projectToDeleteId));
            addToast("success", "Projet supprimé !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la suppression du projet.", "top_right", 3000);
            }
        }
        setProjectToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    function handleCancelDelete() {
        setProjectToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    async function handleStatusChange(projectId: string, statusId: string) {
        if (!token) return;
        try {
            await apiClient.UpdateProjectStatusEndpoint({
                pathParams: { projectId },
                body: { statusId: parseInt(statusId) }
            });
            setRefreshKey(k => k + 1);
            addToast("success", "Statut du projet mis à jour !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour du statut du projet.", "top_right", 3000);
            }
        }
    }

    function handleAddProject() {
        if (directCustomerCount === 0) {
            addToast("error", "Ajoutez un client direct pour pouvoir créer un projet.", "top_right", 3000);
            return;
        }
        setIsAddModalVisible(true);
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Projets" pageSubtitle="Gérez vos projets de traduction" button={<Button variant="blue" name="Ajouter un projet" width="default" type="button" onClick={handleAddProject}><PlusSignIcon /></Button>}></PageHeader>
                <ProjectDataTable projects={projects} projectStatuses={projectStatuses} onAdd={() => setIsAddModalVisible(true)} onEdit={(id) => handleUpdate(id)} onStatusChange={(projectId, statusId) => handleStatusChange(projectId, statusId)} onDelete={(id) => handleDelete(id)} />
                <AddProjectModal isVisible={isAddModalVisible} onClose={() => setIsAddModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
                {projectToUpdate && (<UpdateProjectModal key={projectToUpdate.id} project={projectToUpdate} isVisible={isUpdateModalVisible} onClose={() => setIsUpdateModalVisible(false)} onSuccess={handleConfirmUpdate} />)}
                <ConfirmationModal isVisible={isDeleteModalVisible} title="Supprimer le projet" message="Voulez-vous vraiment supprimer ce projet ?" onConfirm={handleConfirmDelete} onCancel={handleCancelDelete} />
            </AppLayout>
        </>
    );
}

export default ProjectsView;