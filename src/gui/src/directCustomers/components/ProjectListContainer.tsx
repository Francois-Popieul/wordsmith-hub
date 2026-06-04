import { useEffect, useState } from "react";
import { useToast } from "../../hooks/useToast";
import * as zod from "zod";
import { schemas } from "../../infrastructure/openApi/client";
import ListContainer from "../../components/ui/ListContainer";
import { PlusSignIcon, ProjectsIcon } from "../../assets/icons/icons";
import Button from "../../components/ui/Button";
import DirectCustomerProjectDataTable from "./DirectCustomerProjectDataTable";
import axios from "axios";
import ConfirmationModal from "../../components/ui/ConfirmationModal";
import { useApiClient } from "../../hooks/useApiClient";
import AddProjectModal from "../../projects/components/AddProjectModal";
import UpdateProjectModal from "../../projects/components/UpdateProjectModal";

interface ProjectListContainerProps {
    directCustomerId: string;
}

function ProjectListContainer({ directCustomerId }: ProjectListContainerProps) {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [projects, setProjects] = useState<zod.infer<typeof schemas.ProjectDto>[]>([]);
    const [projectToDeleteId, setProjectToDeleteId] = useState<string | null>(null);
    const [isDeleteProjectModalVisible, setIsDeleteProjectModalVisible] = useState(false);
    const [isAddProjectModalVisible, setIsAddProjectModalVisible] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);
    const [projectToUpdate, setProjectToUpdate] = useState<zod.infer<typeof schemas.ProjectDto> | null>(null);
    const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);

    useEffect(() => {
        if (!token) return;
        const fetchProjects = async () => {
            try {
                const response = await apiClient.GetAllProjectsByDirectCustomerEndpoint({ pathParams: { directCustomerId } });
                setProjects(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement des projets.", "top_right", 3000);
                }
            }
        }
        fetchProjects();
    }, [apiClient, directCustomerId, token, addToast, refreshKey]);

    function handleAddProject() {
        setIsAddProjectModalVisible(true);
    }

    function handleEditProject(id: string) {
        const project = projects.find(p => p.id === id) || null;
        setProjectToUpdate(project);
        setIsUpdateModalVisible(true);
    }

    function handleConfirmProjectUpdate() {
        setIsAddProjectModalVisible(false);
        setProjectToUpdate(null);
        setRefreshKey(prev => prev + 1);
    }

    function handleDeleteProject(id: string) {
        setProjectToDeleteId(id);
        setIsDeleteProjectModalVisible(true);
    }
    function handleConfirmProjectDelete() {
        if (!projectToDeleteId) {
            addToast("error", "Aucun projet à supprimer.", "top_right", 3000);
            return;
        }

        try {
            apiClient.DeleteProjectEndpoint({ pathParams: { projectId: projectToDeleteId } });
            addToast("success", "Projet supprimé.", "top_right", 3000);
            setRefreshKey(prev => prev + 1);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la suppression du projet.", "top_right", 3000);
            }
        }
        setIsDeleteProjectModalVisible(false);
        setProjectToDeleteId(null);
        setRefreshKey(prev => prev + 1);
    }

    function handleCancelProjectDelete() {
        setIsDeleteProjectModalVisible(false);
        setProjectToDeleteId(null);
    }

    return (
        <>
            <ListContainer
                icon={<ProjectsIcon />}
                title="Projets"
                presentation="Liste des projets associés à ce client"
                add_button_name="Ajouter un projet"
                no_content_message="Aucun projet enregistré pour le moment."
                no_content_button={<Button name="Ajouter votre premier projet" variant="light" width="default" type="button" onClick={handleAddProject}><PlusSignIcon /></Button>}
                list_length={projects.length}
                onClickAdd={handleAddProject}
            >
                <DirectCustomerProjectDataTable projects={projects} onEdit={handleEditProject} onDelete={handleDeleteProject} />
            </ListContainer>
            <AddProjectModal isVisible={isAddProjectModalVisible} onClose={() => setIsAddProjectModalVisible(false)} onSuccess={() => setRefreshKey(prev => prev + 1)} />
            {projectToUpdate && (
                <UpdateProjectModal key={projectToUpdate?.id} project={projectToUpdate} isVisible={isUpdateModalVisible} onClose={() => setIsUpdateModalVisible(false)} onSuccess={handleConfirmProjectUpdate} />
            )}
            <ConfirmationModal isVisible={isDeleteProjectModalVisible} title="Supprimer le projet" message="Voulez-vous vraiment supprimer ce projet ?" onConfirm={handleConfirmProjectDelete} onCancel={handleCancelProjectDelete} />
        </>
    );
}

export default ProjectListContainer;