import { useEffect, useState } from "react";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import axios from "axios";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormContainer from "../../components/ui/FormContainer";
import { BriefcaseIcon, BuildingIcon, LanguageIcon, ProfileIcon } from "../../assets/icons/icons";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import CheckboxOption from "../../components/ui/CheckboxOption";
import "../../stylesheets/profile_view.css";
import { personalDataSchema } from "../../types/PersonalData";
import * as zod from "zod";
import { addressSchema } from "../../types/Address";
import { useToast } from "../../hooks/useToast";
import LegalStatusListContainer from "../components/LegalStatusListContainer";
import BankAcountListContainer from "../components/BankAcountListContainer";
import { useNavigate } from "react-router";
import Label from "../components/Label";
import { useApiClient } from "../../hooks/useApiClient";
import type { schemas } from "../../infrastructure/openApi/client";
import { useStaticTables } from "../../hooks/useStaticTables";

function ProfileView() {
    const { token, apiClient } = useApiClient();
    const navigate = useNavigate();
    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const [profileData, setProfileData] = useState<zod.infer<typeof schemas.ProfileDto> | void>();
    const [savedProfileData, setSavedProfileData] = useState<zod.infer<typeof schemas.ProfileDto> | void>();
    const {countries, languages, services} = useStaticTables();
    const [editingForm, setEditingForm] = useState<string | null>(null);
    const { addToast } = useToast();

    useEffect(() => {
        if (!token) return;
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                const profileData: zod.infer<typeof schemas.ProfileDto> = response;
                setProfileData(profileData);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement des données de profil.", "top_right", 3000);
                }
            }
        };
        fetchProfileData();
    }, [apiClient, addToast, token]);



    if (!token) {
        navigate("/");
        return null;
    }

    function handleModifyPersonalData() {
        setSavedProfileData(profileData);
        setEditingForm("personal");
    }

    function handleCancelPersonalData() {
        setProfileData(savedProfileData);
        setEditingForm(null);
        setFieldErrors({});
    }

    async function handleSubmitPersonalData(e: React.SyntheticEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const personalData: zod.infer<typeof schemas.UpdateFreelancePersonalDataRequest> = {
            firstName: (formData.get("firstName") as string).trim(),
            lastName: (formData.get("lastName") as string).trim(),
            email: (formData.get("email") as string).trim(),
            phone: (formData.get("phone") as string | null)?.trim() || null
        };

        const validationResult = personalDataSchema.safeParse(personalData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.UpdateFreelancePersonalDataEndpoint({ body: { ...personalData }, pathParams: { freelanceId: profileData?.id || 0 } });
            setEditingForm(null);
            addToast("success", "Données personnelles mises à jour.", "top_right", 3000);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                setFieldErrors(data.errors || {});
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour des données personnelles.", "top_right", 3000);
            }
        }
    }

    function handleModifyAddressData() {
        setSavedProfileData(profileData);
        setProfileData(prev => {
            if (!prev || prev.address) return prev;
            return { ...prev, address: { streetInfo: "", addressComplement: null, postCode: "", city: "", state: null, countryId: 0 } };
        });
        setEditingForm("address");
    }

    function handleCancelAddressData() {
        setProfileData(savedProfileData);
        setEditingForm(null);
        setFieldErrors({});
    }

    async function handleSubmitAddressData(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);
        const addressData: zod.infer<typeof schemas.AddressDto> = {
            streetInfo: (formData.get("streetInfo") as string).trim(),
            addressComplement: (formData.get("addressComplement") as string | null)?.trim() || null,
            postCode: (formData.get("postCode") as string).trim(),
            state: (formData.get("state") as string | null)?.trim() || null,
            city: (formData.get("city") as string).trim(),
            countryId: parseInt(formData.get("countryId") as string, 10),
        };

        const validationResult = addressSchema.safeParse(addressData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.UpdateFreelanceAddressEndpoint({ body: { address: addressData }, pathParams: { freelanceId: profileData?.id || 0 } });
            addToast("success", "Adresse mise à jour.", "top_right", 3000);
            setEditingForm(null);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                setFieldErrors(data.errors || {});
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour de l’adresse.", "top_right", 3000);
            }
        }
    }

    function handleModifyLanguages() {
        setSavedProfileData(profileData);
        setEditingForm("languages");
    }

    function handleCancelLanguages() {
        setProfileData(savedProfileData);
        setEditingForm(null);
        setFieldErrors({});
    }

    async function handleSubmitLanguages(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        if (profileData?.sourceLanguages.length === 0 || profileData?.targetLanguages.length === 0) {
            setFieldErrors({ languages: ["Veuillez sélectionner au moins une langue source et une langue cible."] });
            return;
        }
        setFieldErrors({});

        try {
            await apiClient.UpdateFreelanceLanguagesEndpoint({ body: { sourceLanguageIds: profileData?.sourceLanguages.map(l => l.id) || [], targetLanguageIds: profileData?.targetLanguages.map(l => l.id) || [] }, pathParams: { freelanceId: profileData?.id || 0 } });
            addToast("success", "Langues mises à jour.", "top_right", 3000);
            setEditingForm(null);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour des langues.", "top_right", 3000);
            }
        }
    }

    function handleModifyService() {
        setSavedProfileData(profileData);
        setEditingForm("services");
    }

    function handleCancelService() {
        setProfileData(savedProfileData);
        setEditingForm(null);
        setFieldErrors({});
    }

    async function handleSubmitServices(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        if (profileData?.services.length === 0) {
            setFieldErrors({ services: ["Veuillez sélectionner au moins un service."] });
            return;
        }
        setFieldErrors({});

        try {
            await apiClient.UpdateFreelanceServicesEndpoint({ body: { serviceIds: profileData?.services.map(s => s.id) || [] }, pathParams: { freelanceId: profileData?.id || 0 } });
            addToast("success", "Services mis à jour.", "top_right", 3000);
            setEditingForm(null);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                const data = error.response.data;
                const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                setFieldErrors(data.errors || {});
                addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
            } else {
                addToast("error", "Une erreur inattendue s’est produite lors de la mise à jour des services.", "top_right", 3000);
            }
        }
    }

    function handleModifyDisabled() {
        addToast("information", "Veuillez d’abord enregistrer ou annuler le formulaire en cours de modification.", "top_right", 3000);
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Paramètres du profil" pageSubtitle="Gérez vos informations personnelles et commerciales"></PageHeader>

                {profileData ? (<>

                    <FormContainer
                        icon={<ProfileIcon />}
                        title="Informations personnelles"
                        presentation="Informations générales sur votre compte"
                        cancel_button_name="Annuler"
                        save_button_name="Enregistrer"
                        modify_button_name="Modifier"
                        isEditing={editingForm === "personal"}
                        modifyDisabled={editingForm !== null}
                        onModify={handleModifyPersonalData}
                        onModifyDisabled={handleModifyDisabled}
                        onCancel={handleCancelPersonalData}
                        onSubmit={handleSubmitPersonalData}>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Prénom"
                                name="firstName"
                                type="text"
                                placeholder="Jean"
                                value={profileData.firstName}
                                readonly={editingForm !== "personal"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, firstName: value } : prev)}
                                error={fieldErrors.firstName?.[0]}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Nom"
                                name="lastName"
                                type="text"
                                placeholder="Dupont"
                                value={profileData.lastName}
                                readonly={editingForm !== "personal"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, lastName: value } : prev)}
                                error={fieldErrors.lastName?.[0]}>
                            </FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="E-mail"
                                name="email"
                                type="email"
                                placeholder="jean.dupont@example.com"
                                value={profileData.email}
                                readonly={editingForm !== "personal"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, email: value } : prev)}
                                error={fieldErrors.email?.[0]}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Téléphone"
                                name="phone"
                                type="tel"
                                placeholder="0123456789"
                                value={profileData.phone || ""}
                                readonly={editingForm !== "personal"}
                                required={false}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, phone: value } : prev)}
                                error={fieldErrors.phone?.[0]}>
                            </FormInputGroup>
                        </div>
                    </FormContainer>

                    <FormContainer
                        icon={<BuildingIcon />}
                        title="Adresse de facturation"
                        presentation="Détails de votre adresse de facturation"
                        cancel_button_name="Annuler"
                        save_button_name="Enregistrer"
                        modify_button_name="Modifier"
                        isEditing={editingForm === "address"}
                        modifyDisabled={editingForm !== null}
                        onModify={handleModifyAddressData}
                        onModifyDisabled={handleModifyDisabled}
                        onCancel={handleCancelAddressData}
                        onSubmit={handleSubmitAddressData}>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Adresse"
                                name="streetInfo"
                                type="text"
                                placeholder="12 avenue des Champs-Élysées"
                                value={profileData.address?.streetInfo || ""}
                                readonly={editingForm !== "address"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, streetInfo: value } : null } : prev)}
                                error={fieldErrors.streetInfo?.[0]}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Complément d’adresse"
                                name="addressComplement"
                                type="text"
                                placeholder="Bâtiment A"
                                value={profileData.address?.addressComplement || ""}
                                readonly={editingForm !== "address          "}
                                required={false}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, addressComplement: value } : null } : prev)}
                                error={fieldErrors.addressComplement?.[0]}>
                            </FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Code postal"
                                name="postCode"
                                type="text"
                                placeholder="75001"
                                value={profileData.address?.postCode || ""}
                                readonly={editingForm !== "address"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, postCode: value } : null } : prev)}
                                error={fieldErrors.postCode?.[0]}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Ville"
                                name="city"
                                type="text"
                                placeholder="Paris"
                                value={profileData.address?.city || ""}
                                readonly={editingForm !== "address"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, city: value } : null } : prev)}
                                error={fieldErrors.city?.[0]}>
                            </FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Région/État"
                                name="state"
                                type="text"
                                required={false}
                                placeholder="Île-de-France"
                                value={profileData.address?.state || ""}
                                readonly={editingForm !== "address"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, state: value } : null } : prev)}
                                error={fieldErrors.state?.[0]}>
                            </FormInputGroup>
                            <FormSelectGroup
                                label="Pays"
                                name="countryId"
                                options={countries.map(country => ({ value: country.id.toString(), name: country.name }))}
                                placeholder="Sélectionnez le pays"
                                selected={profileData.address?.countryId ? profileData.address.countryId.toString() : ""}
                                disabled={editingForm !== "address"}
                                required={true}
                                onChange={(value) => {
                                    setProfileData(prev => {
                                        if (!prev) return prev;
                                        return {
                                            ...prev,
                                            address: prev.address
                                                ? { ...prev.address, countryId: parseInt(value) }
                                                : null
                                        };
                                    });
                                }}>
                            </FormSelectGroup>
                        </div>
                    </FormContainer>
                    <section className="multiple_form_section">
                        <FormContainer
                            icon={<LanguageIcon />}
                            title="Compétences linguistiques"
                            presentation="Langues de travail que vous utilisez"
                            cancel_button_name="Annuler"
                            save_button_name="Enregistrer"
                            modify_button_name="Modifier"
                            isEditing={editingForm === "languages"}
                            modifyDisabled={editingForm !== null}
                            onModify={handleModifyLanguages}
                            onModifyDisabled={handleModifyDisabled}
                            onCancel={handleCancelLanguages}
                            onSubmit={handleSubmitLanguages}>
                            <div className="language_container">
                                <div className="source_language_container">
                                    <div>
                                        <h3 className="language_title">Langues source</h3>
                                        {profileData.sourceLanguages.length > 0 ? <div className="label_list">{profileData.sourceLanguages.map(language => (
                                            <Label key={language.id} name={language.name} />
                                        ))}</div> : (editingForm !== "languages" ? <p className="no_selection">Aucune langue sélectionnée</p> : null)}
                                    </div>
                                    {editingForm === "languages" &&
                                        <div className="language_list">{
                                            languages.map(language => (
                                                <CheckboxOption
                                                    key={language.id}
                                                    name={`source-language-${language.id}`}
                                                    label={language.name}
                                                    checked={profileData.sourceLanguages.some(l => l.id === language.id)}
                                                    disabled={editingForm !== "languages"}
                                                    onChange={() => {
                                                        setProfileData(prev => {
                                                            if (!prev) return prev;
                                                            const isSelected = prev.sourceLanguages.some(l => l.id === language.id);
                                                            return {
                                                                ...prev,
                                                                sourceLanguages: isSelected
                                                                    ? prev.sourceLanguages.filter(l => l.id !== language.id)
                                                                    : [...prev.sourceLanguages, language]
                                                            };
                                                        });
                                                    }}
                                                />
                                            ))
                                        }
                                        </div>
                                    }
                                </div>
                                <div className="target_language_container">
                                    <div>
                                        <h3 className="language_title">Langues cible</h3>
                                        {profileData.targetLanguages.length > 0 ? <div className="label_list">{profileData.targetLanguages.map(language => (
                                            <Label key={language.id} name={language.name} />
                                        ))}</div> : (editingForm !== "languages" ? <p className="no_selection">Aucune langue sélectionnée</p> : null)}
                                    </div>
                                    {editingForm === "languages" &&
                                        <div className="language_list">{
                                            languages.map(language => (
                                                <CheckboxOption
                                                    key={language.id}
                                                    name={`target-language-${language.id}`}
                                                    label={language.name}
                                                    checked={profileData.targetLanguages.some(l => l.id === language.id)}
                                                    disabled={editingForm !== "languages"}
                                                    onChange={() => {
                                                        setProfileData(prev => {
                                                            if (!prev) return prev;
                                                            const isSelected = prev.targetLanguages.some(l => l.id === language.id);
                                                            return {
                                                                ...prev,
                                                                targetLanguages: isSelected
                                                                    ? prev.targetLanguages.filter(l => l.id !== language.id)
                                                                    : [...prev.targetLanguages, language]
                                                            };
                                                        });
                                                    }}
                                                />
                                            ))
                                        }
                                        </div>
                                    }
                                </div>
                            </div>
                            {fieldErrors.languages && <p className="form_error_message">{fieldErrors.languages[0]}</p>}
                        </FormContainer>

                        <FormContainer
                            icon={<BriefcaseIcon />}
                            title="Services"
                            presentation="Services que vous proposez"
                            cancel_button_name="Annuler"
                            save_button_name="Enregistrer"
                            modify_button_name="Modifier"
                            isEditing={editingForm === "services"}
                            modifyDisabled={editingForm !== null}
                            onModify={handleModifyService}
                            onModifyDisabled={handleModifyDisabled}
                            onCancel={handleCancelService}
                            onSubmit={handleSubmitServices}>
                            <div className="service_container">
                                <div>
                                    <h3 className="language_title">&nbsp;</h3>
                                    {profileData.services.length > 0 ? <div className="label_list">{profileData.services.map(service => <Label key={service.id} name={service.name} />)}</div> : (editingForm !== "services" ? <p className="no_selection">Aucun service sélectionné</p> : null)}
                                </div>
                                {editingForm === "services" && <div className="service_list">{
                                    services.map(service => (
                                        <CheckboxOption
                                            key={service.id}
                                            name={`service-${service.id}`}
                                            label={service.name}
                                            checked={profileData.services.some(s => s.id === service.id)}
                                            disabled={editingForm !== "services"}
                                            onChange={() => {
                                                setProfileData(prev => {
                                                    if (!prev) return prev;
                                                    const isSelected = prev.services.some(s => s.id === service.id);
                                                    return {
                                                        ...prev,
                                                        services: isSelected
                                                            ? prev.services.filter(s => s.id !== service.id)
                                                            : [...prev.services, service]
                                                    };
                                                });
                                            }}
                                        />
                                    ))
                                }</div>}
                            </div>
                            {fieldErrors.services && <p className="form_error_message">{fieldErrors.services[0]}</p>}
                        </FormContainer>
                    </section>
                    <BankAcountListContainer />
                    <LegalStatusListContainer />
                </>

                ) : (
                    <p>Chargement des données du profil…</p>
                )}
            </AppLayout>
        </>
    );
}

export default ProfileView;
