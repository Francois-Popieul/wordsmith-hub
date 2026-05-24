import { PlusSignIcon } from "../../assets/icons/icons";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import { Navigate } from "react-router";
import AddProjectModal from "../components/AddProjectModal";
import { useEffect, useMemo, useState } from "react";
import ProjectDataTable from "../components/ProjectDataTable";
import { createApiClient, schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import * as zod from "zod";
import axios from "axios";
import ConfirmationModal from "../../components/ui/ConfirmationModal";

function ProjectsView() {
    const token = localStorage.getItem("wshToken");
    const [isAddModalVisible, setIsAddModalVisible] = useState(false);
    const [projects, setProjects] = useState<zod.infer<typeof schemas.ProjectDto>[]>([]);
    // const [projectToUpdate, setProjectToUpdate] = useState<zod.infer<typeof schemas.ProjectDto> | null>(null);
    const [projectToDeleteId, setProjectToDeleteId] = useState<string | null>(null);
    // const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);
    const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const { addToast } = useToast();

    useEffect(() => {
        if (!token) return;
        const fetchProjects = async () => {
            try {
                const response = await apiClient.GetAllProjectsEndpoint();
                setProjects(response);
            } catch (error) {
                if (error instanceof zod.ZodError) {
                    // 204 No Content: HTTP succeeded but the auto-generated schema can't parse an empty body
                } else if (axios.isAxiosError(error) && error.response) {
                    addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des projets.", "top_right", 3000);
                }
            }
        };
        fetchProjects();
    }, [apiClient, addToast, token, refreshKey]);

    if (!token) {
        return <Navigate to="/" />;
    }

    function handleUpdate(id: string) {
        // const project = projects.find(p => p.id === id) || null;
        // setProjectToUpdate(project);
        // setIsUpdateModalVisible(true);
        addToast("information", id, "top_right", 3000);
        addToast("information", "La modification des projets n’est pas encore disponible.", "top_right", 3000);
    }

    function handleDelete(id: string) {
        setProjectToDeleteId(id);
        setIsDeleteModalVisible(true);
    }

    async function handleConfirmDelete() {
        if (projectToDeleteId) {
            try {
                await apiClient.DeleteProjectEndpoint({
                    pathParams: { projectId: projectToDeleteId },
                });
                setProjects(prev => prev.filter(p => p.id !== projectToDeleteId));
                addToast("success", "Projet supprimé !", "top_right", 3000);
            } catch (error) {
                if (error instanceof zod.ZodError) {
                    // 204 No Content: HTTP succeeded but the auto-generated schema can't parse an empty body
                    setProjects(prev => prev.filter(p => p.id !== projectToDeleteId));
                    addToast("success", "Projet supprimé !", "top_right", 3000);
                } else if (axios.isAxiosError(error) && error.response) {
                    addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors de la suppression du projet.", "top_right", 3000);
                }
            }
        }
        setProjectToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    function handleCancelDelete() {
        setProjectToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Projets" pageSubtitle="Gérez vos projets de traduction" button={<Button variant="blue" name="Ajouter un projet" width="default" type="button" onClick={() => setIsAddModalVisible(true)}><PlusSignIcon /></Button>}></PageHeader>
                <ProjectDataTable projects={projects} onAdd={() => setIsAddModalVisible(true)} onEdit={(id) => handleUpdate(id)} onDelete={(id) => handleDelete(id)} />
                <AddProjectModal isVisible={isAddModalVisible} onClose={() => setIsAddModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
                <ConfirmationModal isVisible={isDeleteModalVisible} title="Supprimer le projet" message="Voulez-vous vraiment supprimer ce projet ?" onConfirm={handleConfirmDelete} onCancel={handleCancelDelete} />
            </AppLayout>
        </>
    );
}

export default ProjectsView;