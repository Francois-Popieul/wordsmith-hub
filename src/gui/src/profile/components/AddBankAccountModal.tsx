import { useMemo, useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { createApiClient } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import { bankAccountSchema, type BankAccount } from "../../types/BankAccount";
import zod from "zod";

interface AddBankAccountModalProps {
    isVisible: boolean;
    onClose: () => void;
}

function AddBankAccountModal({ isVisible, onClose }: AddBankAccountModalProps) {
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
        const bankAccountData: BankAccount = {
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
            addToast("success", "Compte bancaire ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
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