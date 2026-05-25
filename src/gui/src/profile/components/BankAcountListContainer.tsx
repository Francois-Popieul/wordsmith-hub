import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { createApiClient, schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import ListContainer from "../../components/ui/ListContainer";
import Button from "../../components/ui/Button";
import { CreditCardIcon, PlusSignIcon } from "../../assets/icons/icons";
import AddBankAccountModal from "./AddBankAccountModal";
import * as zod from "zod";
import BankAccountDataTable from "./BankAccountDataTable";
import ConfirmationModal from "../../components/ui/ConfirmationModal";

function BankAcountListContainer() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const [bankAccounts, setBankAccounts] = useState<zod.infer<typeof schemas.BankAccountDto>[]>([]);
    const { addToast } = useToast();
    const [isAddBankAccountModalVisible, setIsAddBankAccountModalVisible] = useState(false);
    const [refreshKey, setRefreshKey] = useState(0);
    const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
    const [bankAccountToDeleteId, setBankAccountToDeleteId] = useState<string | null>(null);
    // const [isUpdateModalVisible, setIsUpdateModalVisible] = useState(false);
    // const [bankAccountToUpdate, setBankAccountToUpdate] = useState<zod.infer<typeof schemas.BankAccountDto> | null>(null);

    useEffect(() => {
        const fetchBankAccounts = async () => {
            try {
                const response = await apiClient.GetAllBankAccountsEndpoint();
                setBankAccounts(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des comptes bancaires.", "top_right", 3000);
                }
            }
        };
        fetchBankAccounts();
    }, [apiClient, addToast, refreshKey]);

    async function handleAddBankAccount() {
        setIsAddBankAccountModalVisible(true);
    }

    /* function handleUpdate(id: string) {
        const bankAccount = bankAccounts.find(c => c.id === id) || null;
        setBankAccountToUpdate(bankAccount);
        setIsUpdateModalVisible(true);
    } */

    function handleDelete(id: string) {
        setBankAccountToDeleteId(id);
        setIsDeleteModalVisible(true);
    }

    function handleCancelDelete() {
        setBankAccountToDeleteId(null);
        setIsDeleteModalVisible(false);
    }

    async function handleDefaultBankChange(id: string) {
        if (!id || !token) return;
        try {
            await apiClient.UpdateDefaultBankAccountEndpoint({
                pathParams: { bankAccountId: id },
            });
            addToast("success", "Compte bancaire par défaut mis à jour !", "top_right", 3000);
            setRefreshKey(k => k + 1);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors du changement du compte bancaire par défaut.", "top_right", 3000);
            }
        }
    }

    async function handleConfirmDelete() {
        if (!bankAccountToDeleteId) return;
        try {
            await apiClient.DeleteBankAccountEndpoint({
                pathParams: { bankAccountId: bankAccountToDeleteId },
            });
            setBankAccounts(prev => prev.filter(c => c.id !== bankAccountToDeleteId));
        } catch (error) {
            if (error instanceof zod.ZodError) {
                // 204 No Content: HTTP succeeded but the auto-generated schema can't parse an empty body
                addToast("success", "Compte bancaire supprimé !", "top_right", 3000);
            } else if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la suppression du compte bancaire.", "top_right", 3000);
            }
        }
        setBankAccountToDeleteId(null);
        setIsDeleteModalVisible(false);
        setRefreshKey(k => k + 1);
    }

    return <>
        <ListContainer
            icon={<CreditCardIcon />}
            title="Comptes bancaires"
            presentation="Gérer vos comptes bancaires pour la facturation"
            add_button_name="Ajouter un compte"
            no_content_message="Aucun compte bancaire enregistré pour le moment."
            no_content_button={<Button name="Ajouter votre premier compte" variant="light" width="default" type="button" onClick={handleAddBankAccount}><PlusSignIcon /></Button>}
            list_length={bankAccounts.length}
            onClickAdd={handleAddBankAccount}
        >
            <BankAccountDataTable bankAccounts={bankAccounts}
                onDefaultBankChange={(id) => handleDefaultBankChange(id)}
                onEdit={(id) => addToast("information", `Modifier le compte bancaire avec l’ID ${id}`, "top_right", 3000)}
                onDelete={(id) => handleDelete(id)}
            />
        </ListContainer>
        <AddBankAccountModal isVisible={isAddBankAccountModalVisible} onClose={() => setIsAddBankAccountModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} />
        {/* <UpdateBankAccountModal isVisible={isUpdateModalVisible} bankAccount={bankAccountToUpdate} onClose={() => setIsUpdateModalVisible(false)} onSuccess={() => setRefreshKey(k => k + 1)} /> */}
        <ConfirmationModal isVisible={isDeleteModalVisible} title="Supprimer le compte" message="Voulez-vous vraiment supprimer ce compte ?" onConfirm={handleConfirmDelete} onCancel={handleCancelDelete} />
    </>
}

export default BankAcountListContainer;