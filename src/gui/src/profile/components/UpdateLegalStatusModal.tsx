import "./AddLegalStatusModal.css";
import { useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import zod from "zod";
import { legalStatusSchema } from "../../types/LegalStatus";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import { useApiClient } from "../../hooks/useApiClient";
import type { schemas } from "../../infrastructure/openApi/client";
import FormNumberInputGroup from "../../components/ui/FormNumberInputGroup";
import { useStaticTables } from "../../hooks/useStaticTables";

interface UpdateLegalStatusModalProps {
    legalStatus: zod.infer<typeof schemas.LegalStatusDto>;
    isVisible: boolean;
    onClose: () => void;
    onSuccess?: () => void;
}

function UpdateLegalStatusModal({ legalStatus, isVisible, onClose, onSuccess }: UpdateLegalStatusModalProps) {
    const { apiClient } = useApiClient();
    const legalStatusTypes = useStaticTables().legalStatusTypes;
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const [selectedLegalStatusType, setSelectedLegalStatusType] = useState<string>(legalStatus.legalStatusType?.id.toString() || "");
    const [vatExemption, setVatExemption] = useState<boolean>(legalStatus.vatExemption);
    const [taxDeductionExemption, setTaxDeductionExemption] = useState<boolean>(legalStatus.taxDeductionExemption);
    const [vatRate, setVatRate] = useState<string>(legalStatus.vatRate?.toString() || "");

    function resetForm() {
        setFieldErrors({});
        setSelectedLegalStatusType("");
        setVatExemption(false);
        setTaxDeductionExemption(false);
        setVatRate("");
    }

    function handleClose() {
        resetForm();
        onClose();
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const legalStatusData: zod.infer<typeof legalStatusSchema> = {
            legalStatusTypeId: parseInt(formData.get("name") as string, 10),
            siret: formData.get("siret") as string || null,
            vatNumber: formData.get("vatNumber") as string || null,
            vatExemption: vatExemption,
            vatRate: vatRate || null,
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
            await apiClient.UpdateLegalStatusEndpoint({
                pathParams: { legalStatusId: legalStatus.id },
                body: {
                    ...legalStatusData,
                    validFrom: `${legalStatusData.validFrom}T00:00:00Z`,
                    validTo: legalStatusData.validTo ? `${legalStatusData.validTo}T00:00:00Z` : null,
                }
            });
            handleClose();
            onSuccess?.();
            addToast("success", "Statut juridique mis à jour !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour du statut juridique.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Mettre à jour le statut juridique" presentation="Mettre à jour les informations du statut juridique" validateButtonText="Mettre à jour le statut" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormSelectGroup name="name" label="Type de statut" placeholder="Sélectionnez un type" selected={selectedLegalStatusType} required options={legalStatusTypes.map(type => ({ value: type.id.toString(), name: type.name }))} onChange={(value) => setSelectedLegalStatusType(value)} />
                    <FormInputGroup name="siret" label="SIRET" type="text" placeholder="12345678901234" value={legalStatus.siret?.toString() ?? ""} required={false} error={fieldErrors.siret ? fieldErrors.siret[0] : undefined} />
                    {!vatExemption && (
                        <>
                            <FormInputGroup name="vatNumber" label="Numéro de TVA" type="text" placeholder="FR12345678901" value={legalStatus.vatNumber?.toString() ?? ""} required={false} error={fieldErrors.vatNumber ? fieldErrors.vatNumber[0] : undefined} />
                            <FormNumberInputGroup name="vatRate" label="Taux de TVA (%)" value={vatRate} placeholder="20" required={false} onChange={(value) => setVatRate(value)} error={fieldErrors.vatRate ? fieldErrors.vatRate[0] : undefined} />
                        </>
                    )}
                    <FormInputGroup name="validFrom" label="Début de validité" type="date" placeholder="" value={legalStatus.validFrom ? new Date(legalStatus.validFrom).toISOString().split("T")[0] : ""} error={fieldErrors.validFrom ? fieldErrors.validFrom[0] : undefined} />
                    <FormInputGroup name="validTo" label="Fin de validité" type="date" placeholder="" value={legalStatus.validTo ? new Date(legalStatus.validTo).toISOString().split("T")[0] : ""} required={false} error={fieldErrors.validTo ? fieldErrors.validTo[0] : undefined} />
                    <span className="checkbox_container">
                        <input type="checkbox" id="vatExemption" name="vatExemption" checked={vatExemption} onChange={(e) => setVatExemption(e.target.checked)} />
                        <div className="checkbox_text">
                            <label htmlFor="vatExemption">Exonération de TVA</label>
                            <p>Cocher cette case si ce statut vous exonère de la TVA</p>
                        </div>
                    </span>
                    {selectedLegalStatusType === "1" && (
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

export default UpdateLegalStatusModal;