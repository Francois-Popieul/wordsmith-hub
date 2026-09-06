import "./AddDirectCustomerModal.css";
import { useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { useToast } from "../../hooks/useToast/useToast";
import axios from "axios";
import { directCustomerSchema } from "../../types/DirectCustomer";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import { useApiClient } from "../../hooks/useApiClient";
import type { schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";
import { useStaticTables } from "../../hooks/useStaticTables/useStaticTables";

interface AddDirectCustomerModalProps {
    isVisible: boolean;
    onClose: () => void;
    onSuccess?: () => void;
}

function AddDirectCustomerModal({ isVisible, onClose, onSuccess }: AddDirectCustomerModalProps) {
    const { token, apiClient } = useApiClient();
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const { countries, currencies } = useStaticTables();
    const [selectedCountryId, setSelectedCountryId] = useState<number | null>(null);
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

    const fetchDirectCustomerNames = async (query: string) => {
        if (!token) return [];
        try {
            const response = await apiClient.GetAllDirectCustomersForAutocompleteEndpoint(
                {
                    query: { UserInput: query }
                });
            return response;
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la récupération des noms de clients directs.", "top_right", 3000);
            }
            return [];
        }
    }

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        if (!token) return;
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const directCustomerData: zod.infer<typeof schemas.AddDirectCustomerRequest> = {
            name: (formData.get("name") as string).trim(),
            code: (formData.get("code") as string).trim(),
            phone: (formData.get("phone") as string)?.trim() || null,
            email: (formData.get("email") as string).trim(),
            address: {
                streetInfo: (formData.get("streetInfo") as string).trim(),
                addressComplement: (formData.get("addressComplement") as string)?.trim() || null,
                postCode: (formData.get("postCode") as string).trim(),
                city: (formData.get("city") as string).trim(),
                state: (formData.get("state") as string)?.trim() || null,
                countryId: selectedCountryId!,
            },
            siretOrSiren: (formData.get("siretOrSiren") as string)?.trim() || null,
            paymentDelay: Number(formData.get("paymentDelay")),
            currencyId: selectedCurrency!,
        };

        const validationResult = directCustomerSchema.safeParse(directCustomerData);
        if (!validationResult.success) {
            const errors: Record<string, string[]> = {};
            for (const issue of validationResult.error.issues) {
                const key = issue.path[issue.path.length - 1]?.toString() ?? "";
                if (key) {
                    errors[key] = [...(errors[key] ?? []), issue.message];
                }
            }
            setFieldErrors(errors);
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
            onSuccess?.();
            addToast("success", "Client direct ajouté !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de l’ajout du client direct.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Ajouter un client" presentation="Ajouter un nouveau client direct" validateButtonText="Ajouter le client" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="name" label="Nom du client" placeholder="ex. Sony Entertainment Europe" type="text" required error={fieldErrors.name} fetchChoices={fetchDirectCustomerNames} />
                    <FormInputGroup name="code" label="Code du client" placeholder="ex. SEE" type="text" required error={fieldErrors.code} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="email" label="E-mail du client" placeholder="ex. contact@sonyeurope.com" type="email" required error={fieldErrors.email} />
                        <FormInputGroup name="phone" label="Téléphone du client" placeholder="ex. +33 1 23 45 67 89" type="text" required={false} error={fieldErrors.phone} />
                    </div>
                    <FormInputGroup name="streetInfo" label="Numéro et nom de rue" placeholder="ex. 123 rue des Champs-Élysées" type="text" required error={fieldErrors.streetInfo} />
                    <FormInputGroup name="addressComplement" label="Complément d’adresse" placeholder="ex. Bâtiment B" type="text" required={false} error={fieldErrors.addressComplement} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="postCode" label="Code postal" placeholder="ex. 75008" type="text" required error={fieldErrors.postCode} />
                        <FormInputGroup name="city" label="Ville" placeholder="ex. Paris" type="text" required error={fieldErrors.city} />
                    </div>
                    <div className="multiple_field_container">
                        <FormInputGroup name="state" label="Région/État" placeholder="ex. Île-de-France" type="text" required={false} error={fieldErrors.state} />
                        <FormSelectGroup name="countryId" label="Pays" options={countries.map(country => ({ value: country.id.toString(), name: country.name }))} placeholder="Sélectionnez le pays" selected={selectedCountryId?.toString() || ""} required={true} onChange={(value) => setSelectedCountryId(parseInt(value))} >
                        </FormSelectGroup>
                    </div>
                    <FormInputGroup name="siretOrSiren" label="Numéro d’immatriculation" placeholder="ex. FR123456789012" type="text" required={false} error={fieldErrors.siretOrSiren} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="paymentDelay" label="Délai de paiement (jours)" placeholder="ex. 30" type="text" required error={fieldErrors.paymentDelay} />
                        <FormSelectGroup name="currency" label="Devise" options={currencies.map(currency => ({ value: currency.id.toString(), name: `${currency.name} (${currency.code})` }))} placeholder="Sélectionnez la devise" selected={selectedCurrency?.toString() || ""} required onChange={(value) => setSelectedCurrency(parseInt(value))} />
                    </div>
                </FormModal>
            )}
        </>
    );
}

export default AddDirectCustomerModal;