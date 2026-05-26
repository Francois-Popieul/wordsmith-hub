import "./DirectCustomerDetails.css";
import * as zod from "zod";
import { createApiClient, type schemas } from "../../infrastructure/openApi/client";
import { BuildingIcon, CalendarIcon, InvoicesIcon, MailIcon, PhoneIcon } from "../../assets/icons/icons";
import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { useToast } from "../../hooks/useToast";

interface DirectCustomerDetailsProps {
    directCustomer: zod.infer<typeof schemas.DirectCustomerDto>;
}

function DirectCustomerDetails({ directCustomer }: DirectCustomerDetailsProps) {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const { addToast } = useToast();
    const [currencies, setCurrencies] = useState<zod.infer<typeof schemas.Currency>[]>([]);

    useEffect(() => {
        const fetchCurrencies = async () => {
            try {
                const response = await apiClient.GetAllCurrenciesEndpoint();
                setCurrencies(response);
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
        fetchCurrencies();
    }, [apiClient, addToast]);

    return <div className="direct_customer_details_container">
        <div className="details_inner_flex_container">
            <div className="details_section">
                <p className="details_section_header"><MailIcon className="details_icon" />Email</p>
                <p className="details_section_content">{directCustomer.email}</p>
            </div>
            <div className="details_section">
                <p className="details_section_header"><BuildingIcon className="details_icon" />Adresse</p>
                <p className="details_section_content">{`${directCustomer.address.streetInfo}, ${directCustomer.address.city}, ${directCustomer.address.postCode}, ${directCustomer.address.countryId}`}</p>
            </div>
        </div>
        <div className="details_inner_flex_container">
            <div className="details_section">
                <p className="details_section_header"><PhoneIcon className="details_icon" />Numéro de téléphone</p>
                <p className="details_section_content">{directCustomer.phone ? directCustomer.phone : ""}</p>
            </div>
            <div className="details_section">
                <p className="details_section_header"><CalendarIcon className="details_icon" />Début de la collaboration</p>
                <p className="details_section_content"></p>
            </div>
        </div>
        <div className="details_inner_flex_container">
            <div className="details_section">
                <p className="details_section_header"><CalendarIcon className="details_icon" />Délai de paiement</p>
                <p className="details_section_content">{directCustomer.paymentDelay}&nbsp;jours</p>
            </div>
            <div className="details_section">
                <p className="details_section_header"><InvoicesIcon className="details_icon" />Devise</p>
                <p className="details_section_content">{directCustomer.currencyId ? currencies.find(c => c.id === directCustomer.currencyId)?.name : ""} {directCustomer.currencyId ? ` (${currencies.find(c => c.id === directCustomer.currencyId)?.symbol})` : ""}</p>
            </div>
        </div>
    </div>;
}

export default DirectCustomerDetails;