import "./AddLegalStatusModal.css";
import { useMemo, useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { createApiClient } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import zod from "zod";
import { legalStatusSchema, type LegalStatus } from "../../types/LegalStatus";
import FormSelectGroup from "../../components/ui/FormSelectGroup";

interface AddLegalStatusModalProps {
    isVisible: boolean;
    onClose: () => void;
}

const LegalStatusTypes = [
    { value: "author", name: "Artiste-auteur" },
    { value: "self-employed", name: "Auto-entrepreneur" }
];

function AddLegalStatusModal({ isVisible, onClose }: AddLegalStatusModalProps) {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const [selectedLegalStatusType, setSelectedLegalStatusType] = useState<string>("");
    const [vatExemption, setVatExemption] = useState<boolean>(false);
    const [taxDeductionExemption, setTaxDeductionExemption] = useState<boolean>(false);

    function resetForm() {
        setSelectedLegalStatusType("");
        setVatExemption(false);
        setTaxDeductionExemption(false);
        setFieldErrors({});
    }

    function handleClose() {
        resetForm();
        onClose();
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const legalStatusData: LegalStatus = {
            name: formData.get("name") as string,
            siret: formData.get("siret") as string || null,
            vatNumber: formData.get("vatNumber") as string || null,
            vatExemption: vatExemption,
            vatRate: formData.get("vatRate") as string || null,
            taxDeductionExemption: taxDeductionExemption,
            validFrom: formData.get("validFrom") as string,
            validTo: formData.get("validTo") as string || null,

        };

        const validationResult = legalStatusSchema.safeParse(legalStatusData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.AddLegalStatusEndpoint({
                body: {
                    ...legalStatusData,
                    validFrom: `${legalStatusData.validFrom}T00:00:00Z`,
                    validTo: legalStatusData.validTo ? `${legalStatusData.validTo}T00:00:00Z` : null,
                }
            });
            handleClose();
            addToast("success", "Statut juridique ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de l’ajout du statut juridique.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Ajouter un statut juridique" presentation="Ajouter un nouveau statut juridique" validateButtonText="Ajouter le statut" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormSelectGroup name="name" label="Type de statut" placeholder="-- Sélectionnez un type --" selected={selectedLegalStatusType} required options={LegalStatusTypes.map(type => ({ value: type.value, name: type.name }))} onChange={(value) => setSelectedLegalStatusType(value)} />
                    <FormInputGroup name="siret" label="SIRET" type="text" placeholder="12345678901234" error={fieldErrors.siret ? fieldErrors.siret[0] : undefined} />
                    <FormInputGroup name="vatNumber" label="Numéro de TVA" type="text" placeholder="FR12345678901" error={fieldErrors.vatNumber ? fieldErrors.vatNumber[0] : undefined} />
                    <FormInputGroup name="vatRate" label="Taux de TVA (%)" type="text" placeholder="20" error={fieldErrors.vatRate ? fieldErrors.vatRate[0] : undefined} />
                    <FormInputGroup name="validFrom" label="Début de validité" type="date" placeholder="" error={fieldErrors.validFrom ? fieldErrors.validFrom[0] : undefined} />
                    <FormInputGroup name="validTo" label="Fin de validité" type="date" placeholder="" required={false} error={fieldErrors.validTo ? fieldErrors.validTo[0] : undefined} />
                    <span className="checkbox_container">
                        <input type="checkbox" id="vatExemption" name="vatExemption" checked={vatExemption} onChange={(e) => setVatExemption(e.target.checked)} />
                        <div className="checkbox_text">
                            <label htmlFor="vatExemption">Exonération de TVA</label>
                            <p>Cocher cette case si ce statut vous exonère de la TVA</p>
                        </div>
                    </span>
                    {selectedLegalStatusType === "author" && (
                        <span className="checkbox_container">
                            <input type="checkbox" id="taxDeductionExemption" name="taxDeductionExemption" checked={taxDeductionExemption} onChange={(e) => setTaxDeductionExemption(e.target.checked)} />
                            <div className="checkbox_text">
                                <label htmlFor="taxDeductionExemption">Dispense de précompte</label>
                                <p>Cocher cette case si ce statut vous dispense du précompte des charges sociales</p>
                            </div>
                        </span>
                    )}
                </FormModal>
            )}
        </>
    );
}

export default AddLegalStatusModal;