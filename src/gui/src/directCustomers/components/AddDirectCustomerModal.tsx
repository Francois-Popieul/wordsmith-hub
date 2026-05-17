import "./AddDirectCustomerModal.css";
import { useEffect, useMemo, useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { createApiClient } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import zod from "zod";
import { directCustomerSchema, type DirectCustomer } from "../../types/DirectCustomer";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import type { Country } from "../../types/Country";

interface AddDirectCustomerModalProps {
    isVisible: boolean;
    onClose: () => void;
}

function AddDirectCustomerModal({ isVisible, onClose }: AddDirectCustomerModalProps) {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const [countries, setCountries] = useState<{ id: number; name: string }[]>([]);
    const [selectedCountryId, setSelectedCountryId] = useState<number | null>(null);
    const [currencies, setCurrencies] = useState<{ id: number; name: string; code: string }[]>([]);
    const [selectedCurrency, setSelectedCurrency] = useState<number | null>(null);

    function resetForm() {
        setFieldErrors({});
        setSelectedCountryId(null);
        setSelectedCurrency(null);
    }

    function handleClose() {
        resetForm();
        onClose();
    }

    useEffect(() => {
        if (!token) return;
        const fetchCountries = async () => {
            try {
                const response = await apiClient.GetAllCountriesEndpoint();
                response.sort((a: Country, b: Country) => a.name.localeCompare(b.name));
                setCountries(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des pays.", "top_right", 3000);
                }
            }
        };
        fetchCountries();
    }, [apiClient, addToast, token]);

    useEffect(() => {
        if (!token) return;
        const fetchCurrencies = async () => {
            try {
                const response = await apiClient.GetAllCurrenciesEndpoint();
                response.sort((a: { id: number; name: string; code: string }, b: { id: number; name: string; code: string }) => a.name.localeCompare(b.name));
                setCurrencies(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des devises.", "top_right", 3000);
                }
            }
        };
        fetchCurrencies();
    }, [apiClient, addToast, token]);

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const directCustomerData: DirectCustomer = {
            name: formData.get("name") as string,
            code: formData.get("code") as string,
            email: formData.get("email") as string,
            phone: formData.get("phone") as string || null,
            address: {
                streetInfo: formData.get("streetInfo") as string,
                addressComplement: formData.get("addressComplement") as string || null,
                postCode: formData.get("postalCode") as string,
                city: formData.get("city") as string,
                state: null,
                countryId: selectedCountryId!,
            },
            siret: formData.get("siret") as string || null,
            paymentDelay: formData.get("paymentDelay") as string,
            currencyId: selectedCurrency!,
        };

        const validationResult = directCustomerSchema.safeParse(directCustomerData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.AddDirectCustomerEndpoint({
                body: {
                    ...directCustomerData
                }
            });
            handleClose();
            addToast("success", "Client direct ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                addToast("error", `Erreur de l’API : ${error.response.data}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de l’ajout du client direct.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Ajouter un client" presentation="Ajouter un nouveau client direct" validateButtonText="Ajouter le client" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="name" label="Nom du client" placeholder="ex. Sony Entertainment Europe" type="text" required error={fieldErrors.name} />
                    <FormInputGroup name="code" label="Code du client" placeholder="ex. SEE" type="text" required error={fieldErrors.code} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="email" label="E-mail du client" placeholder="ex. contact@sonyeurope.com" type="email" required error={fieldErrors.email} />
                        <FormInputGroup name="phone" label="Téléphone du client" placeholder="ex. +33 1 23 45 67 89" type="text" required={false} error={fieldErrors.phone} />
                    </div>
                    <FormInputGroup name="streetInfo" label="Numéro et nom de rue" placeholder="ex. 123 rue des Champs-Élysées" type="text" required error={fieldErrors.streetInfo} />
                    <FormInputGroup name="addressComplement" label="Complément d’adresse" placeholder="ex. Bâtiment B" type="text" required={false} error={fieldErrors.addressComplement} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="postalCode" label="Code postal" placeholder="ex. 75008" type="text" required error={fieldErrors.postalCode} />
                        <FormInputGroup name="city" label="Ville" placeholder="ex. Paris" type="text" required error={fieldErrors.city} />
                    </div>
                    <div className="multiple_field_container">
                        <FormInputGroup name="state" label="Région/État" placeholder="ex. Île-de-France" type="text" required={false} error={fieldErrors.state} />
                        <FormSelectGroup name="countryId" label="Pays" options={countries.map(country => ({ value: country.id.toString(), name: country.name }))} placeholder="-- Sélectionnez le pays --" selected={selectedCountryId?.toString() || ""} required={true} onChange={(value) => setSelectedCountryId(parseInt(value))} >
                        </FormSelectGroup>
                    </div>
                    <FormInputGroup name="siret" label="SIRET" placeholder="ex. FR123456789012" type="text" required={false} error={fieldErrors.siret} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="paymentDelay" label="Délai de paiement (jours)" placeholder="ex. 30" type="text" required error={fieldErrors.paymentDelay} />
                        <FormSelectGroup name="currency" label="Devise" options={currencies.map(currency => ({ value: currency.id.toString(), name: `${currency.name} (${currency.code})` }))} placeholder="-- Sélectionnez la devise --" selected={selectedCurrency?.toString() || ""} required onChange={(value) => setSelectedCurrency(parseInt(value))} />
                    </div>
                </FormModal>
            )}
        </>
    );
}

export default AddDirectCustomerModal;