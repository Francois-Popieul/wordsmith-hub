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

interface ProjectListContainerProps {
    directCustomerId: string;
}

function ProjectListContainer({ directCustomerId }: ProjectListContainerProps) {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [projects, setProjects] = useState<zod.infer<typeof schemas.ProjectDto>[]>([]);
    const [projectToDeleteId, setProjectToDeleteId] = useState<string | null>(null);
    const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);

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
    }, [apiClient, directCustomerId, token, addToast]);

    function handleAddProject() {
        addToast("information", "La fonctionnalité d’ajout de projet est en cours de développement.", "top_right", 3000);
    }

    function handleEditProject(id: string) {
        console.log("Edit project with id:", id);
    }

    function handleDeleteProject(id: string) {
        setProjectToDeleteId(id);
        setIsDeleteModalVisible(true);
    }
    function handleConfirmDelete() {
        if (!projectToDeleteId) {
            addToast("error", "Aucun projet à supprimer.", "top_right", 3000);
            return;
        }
    }

    function handleCancelDelete() {
        setIsDeleteModalVisible(false);
        setProjectToDeleteId(null);
        addToast("information", "Suppression du projet annulée.", "top_right", 3000);
    }

    return (
        <>
            <ListContainer
                icon={<ProjectsIcon />}
                title="Projets"
                presentation="Liste des projets associés à ce client"
                add_button_name="Ajouter un projet"
                no_content_message="Aucun projet enregistré pour le moment. Cliquez sur le bouton ci-dessous afin de définir vos projets."
                no_content_button={<Button name="Ajouter votre premier projet" variant="light" width="default" type="button" onClick={handleAddProject}><PlusSignIcon /></Button>}
                list_length={projects.length}
                onClickAdd={handleAddProject}
            >
                <DirectCustomerProjectDataTable projects={projects} onEdit={handleEditProject} onDelete={handleDeleteProject} />
            </ListContainer>
            <ConfirmationModal isVisible={isDeleteModalVisible} title="Supprimer le projet" message="Voulez-vous vraiment supprimer ce projet ?" onConfirm={handleConfirmDelete} onCancel={handleCancelDelete} />
        </>
    );
}

export default ProjectListContainer;