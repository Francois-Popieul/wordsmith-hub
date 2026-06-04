import { useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import { bankAccountSchema } from "../../types/BankAccount";
import * as zod from "zod";
import { useApiClient } from "../../hooks/useApiClient";
import type { schemas } from "../../infrastructure/openApi/client";

interface AddBankAccountModalProps {
    isVisible: boolean;
    onClose: () => void;
    onSuccess?: () => void;
}

function AddBankAccountModal({ isVisible, onClose, onSuccess }: AddBankAccountModalProps) {
    const { token, apiClient } = useApiClient();
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
        if (!token) return;
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const bankAccountData: zod.infer<typeof schemas.AddBankAccountRequest> = {
            label: formData.get("label") as string,
            bankName: formData.get("bankName") as string,
            accountHolderName: formData.get("accountHolderName") as string,
            iban: formData.get("iban") as string,
            bic: formData.get("bic") as string,
        };

        const validationResult = bankAccountSchema.safeParse(bankAccountData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.AddBankAccountEndpoint({
                body: {
                    ...bankAccountData
                }
            });
            handleClose();
            onSuccess?.();
            addToast("success", "Compte bancaire ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de l’ajout du compte bancaire.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Ajouter un compte bancaire" presentation="Ajouter un nouveau compte bancaire pour recevoir des paiements" validateButtonText="Ajouter le compte" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="label" label="Intitulé du compte" placeholder="ex. Compte principal, Compte en euros" type="text" required error={fieldErrors.label} />
                    <FormInputGroup name="bankName" label="Nom de la banque" placeholder="ex. BNP Paribas" type="text" required error={fieldErrors.bankName} />
                    <FormInputGroup name="accountHolderName" label="Nom du titulaire" placeholder="Jean Dupont" type="text" required error={fieldErrors.accountHolderName} />
                    <FormInputGroup name="iban" label="Code IBAN" placeholder="ex. FR76 NWBK 6016 1331 9268 19" type="text" required error={fieldErrors.iban} />
                    <FormInputGroup name="bic" label="Code BIC" placeholder="ex. BNPA FR PP XXX" type="text" required error={fieldErrors.bic} />
                </FormModal>
            )}
        </>
    );
}

export default AddBankAccountModal;