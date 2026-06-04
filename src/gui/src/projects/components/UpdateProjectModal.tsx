import { useEffect, useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import * as zod from "zod";
import FormMultiSelectGroup from "../../components/ui/FormMultiSelectGroup";
import { useApiClient } from "../../hooks/useApiClient";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import { useStaticTables } from "../../hooks/useStaticTables";

interface UpdateProjectModalProps {
    isVisible: boolean;
    project: zod.infer<typeof schemas.ProjectDto> | null;
    onClose: () => void;
    onSuccess?: () => void;
}

function UpdateProjectModal({ isVisible, project, onClose, onSuccess }: UpdateProjectModalProps) {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [selectedDirectCustomerIds, setSelectedDirectCustomerIds] = useState<string[] | null>(project?.directCustomers?.map(c => c.id) ?? []);
    const [selectedDomain, setSelectedDomain] = useState<string | null>(project?.domain ?? "");
    const [directCustomers, setDirectCustomers] = useState<zod.infer<typeof schemas.DirectCustomerDto>[]>([]);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const domainTypes = useStaticTables().domainTypes;

    useEffect(() => {
        if (!token) return;
        const fetchDirectCustomers = async () => {
            try {
                const response = await apiClient.GetAllDirectCustomersEndpoint();
                setDirectCustomers(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des clients directs.", "top_right", 3000);
                }
            }
        };
        fetchDirectCustomers();
    }, [apiClient, addToast, token]);


    function resetForm() {
        setSelectedDomain(null);
        setSelectedDirectCustomerIds([]);
        setFieldErrors({});
    }

    function handleClose() {
        resetForm();
        onClose();
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        if (!token) return;
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const projectData = {
            name: formData.get("name") as string,
            directCustomerIds: selectedDirectCustomerIds,
            domain: formData.get("domain") as string,
            endCustomerName: formData.get("endCustomerName") as string,
            description: formData.get("description") as string,
        };

        const validationResult = schemas.AddProjectRequest.safeParse(projectData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.UpdateProjectEndpoint({
                pathParams: { projectId: project?.id ?? "" },
                body: {
                    ...projectData,
                }
            });
            handleClose();
            onSuccess?.();
            addToast("success", "Projet mis à jour !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour du projet.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Modifier le projet" presentation="Mettre à jour les informations du projet" validateButtonText="Mettre à jour le projet" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="name" label="Nom du projet" placeholder="ex. Site web de Sony" value={project?.name ?? ""} type="text" required error={fieldErrors.name} />
                    <FormMultiSelectGroup
                        name="directCustomerIds"
                        label="Clients"
                        placeholder="Sélectionnez les clients"
                        options={directCustomers.map(c => ({ value: c.id, name: c.name }))}
                        selected={selectedDirectCustomerIds ?? []}
                        required
                        error={fieldErrors.directCustomerIds}
                        onChange={(values) => setSelectedDirectCustomerIds(values)}
                    />
                    <FormSelectGroup name="domain" label="Domaine" placeholder="Sélectionnez le domaine" selected={selectedDomain ? domainTypes.find(domain => domain.code === selectedDomain)?.code : ""} options={domainTypes.map(domain => ({ value: domain.code, name: domain.name }))} required onChange={(value) => setSelectedDomain(value)} />
                    <FormInputGroup name="endCustomerName" label="Client final" placeholder="ex. Société XYZ" type="text" value={project?.endCustomer?.name ?? ""} required={false} error={fieldErrors.endCustomerName} />
                    <FormInputGroup name="description" label="Description" placeholder="ex. Description du projet" value={project?.description ?? ""} type="text" required={false} error={fieldErrors.description} />
                </FormModal>
            )}
        </>
    );
}

export default UpdateProjectModal;