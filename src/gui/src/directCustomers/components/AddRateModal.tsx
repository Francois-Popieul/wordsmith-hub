import "./AddRateModal.css";
import { useState } from "react";
import FormModal from "../../components/ui/FormModal";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import * as zod from "zod";
import { rateSchema } from "../../types/Rate";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import { useApiClient } from "../../hooks/useApiClient";
import FormNumberInputGroup from "../../components/ui/FormNumberInputGroup";
import type { schemas } from "../../infrastructure/openApi/client";
import { usePricingUnits } from "../../hooks/useStaticData";

interface AddRateModalProps {
    directCustomerId: string;
    directCustomerCurrencySign: string;
    freelanceProfile: zod.infer<typeof schemas.ProfileDto> | null;
    isVisible: boolean;
    onClose: () => void;
    onSuccess?: () => void;
}

function AddRateModal({ directCustomerId, directCustomerCurrencySign, freelanceProfile, isVisible, onClose, onSuccess }: AddRateModalProps) {
    const { token, apiClient } = useApiClient();
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const [selectedServiceId, setSelectedServiceId] = useState<string>("");
    const [selectedSourceLanguageId, setSelectedSourceLanguageId] = useState<string>("");
    const [selectedTargetLanguageId, setSelectedTargetLanguageId] = useState<string>("");
    const [unitPrice, setUnitPrice] = useState<number | null>(null);
    const [selectedUnit, setSelectedUnit] = useState<string>("");
    const pricingUnits = usePricingUnits();

    function resetForm() {
        setFieldErrors({});
        setSelectedServiceId("");
        setSelectedSourceLanguageId("");
        setSelectedTargetLanguageId("");
        setSelectedUnit("");
        setUnitPrice(null);
    }

    function handleClose() {
        resetForm();
        onClose();
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        if (!token) return;
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const rateData: zod.infer<typeof schemas.AddRateRequest> = {
            unitPrice: unitPrice !== null ? unitPrice : 0,
            unit: formData.get("unit") as string,
            sourceLanguageId: formData.get("sourceLanguageId") ? parseInt(formData.get("sourceLanguageId") as string) : 0,
            targetLanguageId: formData.get("targetLanguageId") ? parseInt(formData.get("targetLanguageId") as string) : 0,
            serviceId: formData.get("serviceId") ? parseInt(formData.get("serviceId") as string) : 0,
            directCustomerId: directCustomerId
        };

        const validationResult = rateSchema.safeParse(rateData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.AddRateEndpoint({
                body: rateData
            });
            handleClose();
            onSuccess?.();
            addToast("success", "Tarif ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de l’ajout du tarif.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Ajouter un tarif" presentation="Ajouter un nouveau tarif pour ce client" validateButtonText="Ajouter le tarif" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormSelectGroup name="serviceId" label="Nom du service" selected={selectedServiceId} options={freelanceProfile?.services.map(service => ({ value: service.id.toString(), name: service.name })) || []} placeholder="Sélectionnez le service" required onChange={(value) => setSelectedServiceId(value)} />
                    <div className="multiple_field_container">
                        <FormSelectGroup name="sourceLanguageId" label="Langue source" selected={selectedSourceLanguageId} options={freelanceProfile?.sourceLanguages.map(language => ({ value: language.id.toString(), name: language.name })) || []} placeholder="Sélectionnez la langue source" required onChange={(value) => setSelectedSourceLanguageId(value)} />
                        <FormSelectGroup name="targetLanguageId" label="Langue cible" selected={selectedTargetLanguageId} options={freelanceProfile?.targetLanguages.map(language => ({ value: language.id.toString(), name: language.name })) || []} placeholder="Sélectionnez la langue cible" required onChange={(value) => setSelectedTargetLanguageId(value)} />
                    </div>
                    <div className="multiple_field_container">
                        <FormNumberInputGroup name="unitPrice" label={`Tarif (${directCustomerCurrencySign})`} value={unitPrice !== null ? unitPrice.toString() : ""} onChange={(value) => setUnitPrice(value ? parseFloat(value) : null)} placeholder="0,0000" required error={fieldErrors.unitPrice ? fieldErrors.unitPrice[0] : undefined} />
                        <FormSelectGroup name="unit" label="Unité" selected={selectedUnit} options={pricingUnits.map(unit => ({ value: unit.code, name: unit.name }))} placeholder="Sélectionnez l’unité" required onChange={(value) => setSelectedUnit(value)} />
                    </div>
                </FormModal>
            )}
        </>
    );
}

export default AddRateModal;