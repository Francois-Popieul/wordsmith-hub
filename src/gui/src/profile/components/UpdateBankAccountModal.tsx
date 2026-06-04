import { useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import { bankAccountSchema } from "../../types/BankAccount";
import * as zod from "zod";
import { useApiClient } from "../../hooks/useApiClient";
import type { schemas } from "../../infrastructure/openApi/client";

interface UpdateBankAccountModalProps {
    bankAccount: zod.infer<typeof schemas.BankAccountDto> | null;
    isVisible: boolean;
    onClose: () => void;
    onSuccess?: () => void;
}

function UpdateBankAccountModal({ bankAccount, isVisible, onClose, onSuccess }: UpdateBankAccountModalProps) {
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
        const bankAccountData: zod.infer<typeof schemas.UpdateBankAccountRequest> = {
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
            if (!bankAccount) return;
            await apiClient.UpdateBankAccountEndpoint({
                pathParams: { bankAccountId: bankAccount?.id },
                body: {
                    ...bankAccountData
                }
            });
            handleClose();
            onSuccess?.();
            addToast("success", "Compte bancaire modifié !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la modification du compte bancaire.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Modifier le compte bancaire" presentation="Modifier les informations du compte bancaire existant" validateButtonText="Modifier le compte" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="label" label="Intitulé du compte" placeholder="ex. Compte principal, Compte en euros" value={bankAccount?.label} type="text" required error={fieldErrors.label} />
                    <FormInputGroup name="bankName" label="Nom de la banque" placeholder="ex. BNP Paribas" value={bankAccount?.bankName} type="text" required error={fieldErrors.bankName} />
                    <FormInputGroup name="accountHolderName" label="Nom du titulaire" placeholder="Jean Dupont" value={bankAccount?.accountHolderName} type="text" required error={fieldErrors.accountHolderName} />
                    <FormInputGroup name="iban" label="Code IBAN" placeholder="ex. FR76 NWBK 6016 1331 9268 19" type="text" required error={fieldErrors.iban} />
                    <FormInputGroup name="bic" label="Code BIC" placeholder="ex. BNPA FR PP XXX" value={bankAccount?.bic} type="text" required error={fieldErrors.bic} />
                </FormModal>
            )}
        </>
    );
}

export default UpdateBankAccountModal;