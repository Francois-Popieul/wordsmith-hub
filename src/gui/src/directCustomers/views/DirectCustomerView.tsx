import { useLocation, useNavigate } from "react-router";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import { useToast } from "../../hooks/useToast/useToast";
import { schemas } from "../../infrastructure/openApi/client";
import { useEffect, useState } from "react";
import * as zod from "zod";
import axios from "axios";
import DirectCustomerDetails from "../components/DirectCustomerDetails";
import RateListContainer from "../components/RateListContainer";
import ProjectListContainer from "../components/ProjectListContainer";
import { useApiClient } from "../../hooks/useApiClient";
import { useStaticTables } from "../../hooks/useStaticTables/useStaticTables";

function DirectCustomerView() {
    const { token, apiClient } = useApiClient();
    const navigate = useNavigate();
    const location = useLocation();
    const directCustomerId: string | undefined = location.pathname.split("/").pop();
    const { addToast } = useToast();
    const [directCustomer, setDirectCustomer] = useState<zod.infer<typeof schemas.DirectCustomerDto> | null>(null);
    const currencies = useStaticTables().currencies;

    useEffect(() => {
        if (!token) return;
        if (!directCustomerId) {
            addToast("error", "ID du client introuvable dans l’URL.", "top_right", 3000);
            return;
        }
        const fetchDirectCustomer = async () => {
            try {
                const response = await apiClient.GetDirectCustomerEndpoint({
                    pathParams: { directCustomerId: directCustomerId }
                });
                setDirectCustomer(response);
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
        fetchDirectCustomer();
    }, [apiClient, addToast, token, directCustomerId]);

    if (!token) {
        navigate("/");
        return null;
    }

    return <AppLayout>
        <PageHeader pageTitle={directCustomer ? directCustomer.name : ""} pageSubtitle="Modifiez les informations du client et vos tarifs avec lui" ></PageHeader>
        {directCustomer ? <DirectCustomerDetails directCustomer={directCustomer} /> : <p>Chargement des informations du client…</p>}
        <RateListContainer directCustomerId={directCustomerId!} directCustomerCurrencySign={directCustomer ? currencies.find(c => c.id === directCustomer.currencyId)?.symbol ?? "" : ""} />
        <ProjectListContainer directCustomerId={directCustomerId!} />
    </AppLayout>;
}

export default DirectCustomerView;