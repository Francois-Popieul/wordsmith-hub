import "./AddDirectCustomerModal.css";
import { useEffect, useMemo, useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { createApiClient } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import zod from "zod";
import { directCustomerSchema, type DirectCustomer } from "../../types/DirectCustomer";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import type { Country } from "../../types/Country";

interface AddProjectModalProps {
    isVisible: boolean;
    onClose: () => void;
}

function AddProjectModal({ isVisible, onClose }: AddProjectModalProps) {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();


    function resetForm() {
        setFieldErrors({});
    }

    function handleClose() {
        resetForm();
        onClose();
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);

        const validationResult = projectSchema.safeParse(projectData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.AddProjectEndpoint({
                body: {
                    ...projectData,
                }
            });
            handleClose();
            addToast("success", "Projet ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de l’ajout du projet.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Ajouter un projet" presentation="Créer un nouveau projet de traduction" validateButtonText="Ajouter le projet" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="name" label="Nom du projet" placeholder="ex. Nouveau site web" type="text" required error={fieldErrors.name} />
                    <FormInputGroup name="domain" label="Domaine" placeholder="ex. Jeu vidéo" type="text" required error={fieldErrors.domain} />
                    <FormInputGroup name="endClient" label="Client final" placeholder="ex. Société XYZ" type="text" required error={fieldErrors.endClient} />
                    <FormInputGroup name="description" label="Description" placeholder="ex. Description du projet" type="text" required error={fieldErrors.description} />
                </FormModal>
            )}
        </>
    );
}

export default AddProjectModal;