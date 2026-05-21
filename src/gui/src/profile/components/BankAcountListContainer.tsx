import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { createApiClient, schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import ListContainer from "../../components/ui/ListContainer";
import Button from "../../components/ui/Button";
import { CreditCardIcon, PlusSignIcon } from "../../assets/icons/icons";
import AddBankAccountModal from "./AddBankAccountModal";
import * as zod from "zod";
import BankAccountDataTables from "./BankAccountDataTables";

function BankAcountListContainer() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const [bankAccounts, setBankAccounts] = useState<zod.infer<typeof schemas.BankAccountDto>[]>([]);
    const { addToast } = useToast();
    const [isAddBankAccountModalVisible, setIsAddBankAccountModalVisible] = useState(false);

    useEffect(() => {
        const fetchBankAccounts = async () => {
            try {
                const response = await apiClient.GetAllBankAccountsEndpoint();
                setBankAccounts(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des comptes bancaires.", "top_right", 3000);
                }
            }
        };
        fetchBankAccounts();
    }, [apiClient, addToast]);

    async function handleAddBankAccount() {
        setIsAddBankAccountModalVisible(true);
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
            <BankAccountDataTables bankAccounts={bankAccounts} onDefaultBankChange={(id) => addToast("information", `Changer le compte bancaire par défaut avec l’ID ${id}`, "top_right", 3000)} onEdit={(id) => addToast("information", `Modifier le compte bancaire avec l’ID ${id}`, "top_right", 3000)} onDelete={(id) => addToast("information", `Supprimer le compte bancaire avec l’ID ${id}`, "top_right", 3000)} />
        </ListContainer>
        <AddBankAccountModal isVisible={isAddBankAccountModalVisible} onClose={() => setIsAddBankAccountModalVisible(false)} />
    </>
}

export default BankAcountListContainer;