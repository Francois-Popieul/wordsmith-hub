import "./AddDirectCustomerModal.css";
import { useState } from "react";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormModal from "../../components/ui/FormModal";
import { schemas } from "../../infrastructure/openApi/client";
import { useToast } from "../../hooks/useToast";
import axios from "axios";
import * as zod from "zod";
import { directCustomerSchema } from "../../types/DirectCustomer";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import { useCountries, useCurrencies } from "../../hooks/useStaticData";
import { useApiClient } from "../../hooks/useApiClient";

interface UpdateDirectCustomerModalProps {
    isVisible: boolean;
    customer: zod.infer<typeof schemas.DirectCustomerDto> | null;
    onClose: () => void;
    onSuccess?: () => void;
}

function UpdateDirectCustomerModal({ isVisible, customer, onClose, onSuccess }: UpdateDirectCustomerModalProps) {
    const { token, apiClient } = useApiClient();
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const { addToast } = useToast();
    const countries = useCountries();
    const currencies = useCurrencies();
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

    async function handleSubmit(event: React.SyntheticEvent<HTMLFormElement>) {
        if (!token) return;
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const directCustomerData: zod.infer<typeof schemas.UpdateDirectCustomerRequest> = {
            name: formData.get("name") as string,
            code: formData.get("code") as string,
            phone: formData.get("phone") as string || null,
            email: formData.get("email") as string,
            address: {
                streetInfo: formData.get("streetInfo") as string,
                addressComplement: formData.get("addressComplement") as string || null,
                postCode: formData.get("postCode") as string,
                city: formData.get("city") as string,
                state: null,
                countryId: (selectedCountryId ?? customer?.address.countryId)!,
            },
            siretOrSiren: formData.get("siretOrSiren") as string || null,
            paymentDelay: Number(formData.get("paymentDelay")),
            currencyId: (selectedCurrency ?? customer?.currencyId)!,
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
            await apiClient.UpdateDirectCustomerEndpoint({
                pathParams: { directCustomerId: customer?.id || "" },
                body: {
                    name: directCustomerData.name,
                    code: directCustomerData.code,
                    email: directCustomerData.email,
                    phone: directCustomerData.phone,
                    address: directCustomerData.address,
                    siretOrSiren: directCustomerData.siretOrSiren,
                    paymentDelay: directCustomerData.paymentDelay,
                    currencyId: directCustomerData.currencyId,
                },
            });
            handleClose();
            onSuccess?.();
            addToast("success", "Client direct mis à jour !", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour du client direct.", "top_right", 3000);
            }
        }
    }

    return (
        <>
            {isVisible && (
                <FormModal title="Mettre à jour un client" presentation="Mettre à jour le client direct" validateButtonText="Mettre à jour le client" onCancel={handleClose} onSubmit={handleSubmit}>
                    <FormInputGroup name="name" label="Nom du client" placeholder="ex. Sony Entertainment Europe" type="text" value={customer?.name} required error={fieldErrors.name} />
                    <FormInputGroup name="code" label="Code du client" placeholder="ex. SEE" type="text" value={customer?.code} required error={fieldErrors.code} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="email" label="E-mail du client" placeholder="ex. contact@sonyeurope.com" type="email" value={customer?.email} required error={fieldErrors.email} />
                        <FormInputGroup name="phone" label="Téléphone du client" placeholder="ex. +33 1 23 45 67 89" type="text" value={customer?.phone?.toString()} required={false} error={fieldErrors.phone} />
                    </div>
                    <FormInputGroup name="streetInfo" label="Numéro et nom de rue" placeholder="ex. 123 rue des Champs-Élysées" type="text" value={customer?.address.streetInfo} required error={fieldErrors.streetInfo} />
                    <FormInputGroup name="addressComplement" label="Complément d’adresse" placeholder="ex. Bâtiment B" type="text" value={customer?.address.addressComplement?.toString()} required={false} error={fieldErrors.addressComplement} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="postCode" label="Code postal" placeholder="ex. 75008" type="text" value={customer?.address.postCode.toString()} required error={fieldErrors.postCode} />
                        <FormInputGroup name="city" label="Ville" placeholder="ex. Paris" type="text" value={customer?.address.city} required error={fieldErrors.city} />
                    </div>
                    <div className="multiple_field_container">
                        <FormInputGroup name="state" label="Région/État" placeholder="ex. Île-de-France" type="text" value={customer?.address.state?.toString()} required={false} error={fieldErrors.state} />
                        <FormSelectGroup name="countryId" label="Pays" options={countries.map(country => ({ value: country.id.toString(), name: country.name }))} placeholder="Sélectionnez le pays" selected={(selectedCountryId ?? customer?.address.countryId)?.toString() ?? ""} required={true} onChange={(value) => setSelectedCountryId(parseInt(value))} >
                        </FormSelectGroup>
                    </div>
                    <FormInputGroup name="siretOrSiren" label="Numéro d’immatriculation" placeholder="ex. FR123456789012" type="text" value={customer?.siretOrSiren?.toString()} required={false} error={fieldErrors.siretOrSiren} />
                    <div className="multiple_field_container">
                        <FormInputGroup name="paymentDelay" label="Délai de paiement (jours)" placeholder="ex. 30" type="text" value={customer?.paymentDelay.toString()} required error={fieldErrors.paymentDelay} />
                        <FormSelectGroup name="currency" label="Devise" options={currencies.map(currency => ({ value: currency.id.toString(), name: `${currency.name} (${currency.code})` }))} placeholder="Sélectionnez la devise" selected={(selectedCurrency ?? customer?.currencyId)?.toString() ?? ""} required onChange={(value) => setSelectedCurrency(parseInt(value))} />
                    </div>
                </FormModal>
            )}
        </>
    );
}

export default UpdateDirectCustomerModal;