import { useEffect, useMemo, useState } from "react";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import { createApiClient } from "../../infrastructure/openApi/client";
import axios from "axios";
import ProfileDto from "../models/ProfileDto";
import FormInputGroup from "../../components/ui/FormInputGroup";
import FormContainer from "../../components/ui/FormContainer";
import { Profile } from "../../assets/icons/icons";
import { BuildingIcon } from "../../assets/icons/icons";
import { LanguageIcon } from "../../assets/icons/icons";
import { BriefcaseIcon } from "../../assets/icons/icons";
import FormSelectGroup from "../../components/ui/FormSelectGroup";
import type { Country } from "../../models/Country";
import type { TranslationLanguage } from "../../models/TranslationLanguage";
import type { Service } from "../../models/Service";
import CheckboxOption from "../../components/ui/CheckboxOption";
import "../../stylesheets/profile_view.css";
import { PersonalDataSchema, type PersonalData } from "../../models/PersonalData";
import * as zod from "zod";

function ProfileView() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);


    const [fieldErrors, setFieldErrors] = useState<Record<string, string[]>>({});
    const [profileData, setProfileData] = useState<ProfileDto | void>();
    const [savedProfileData, setSavedProfileData] = useState<ProfileDto | void>();
    const [countries, setCountries] = useState<Country[]>([]);
    const [languages, setLanguages] = useState<TranslationLanguage[]>([]);
    const [services, setServices] = useState<Service[]>([]);
    const [editingForm, setEditingForm] = useState<string | null>(null);

    useEffect(() => {
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                console.log("Profile data:", response);
                const profileData = new ProfileDto(response.id, response.firstName, response.lastName, response.email, response.phone, response.address, response.statusId, response.sourceLanguages, response.targetLanguages, response.services);
                setProfileData(profileData);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    console.error("Erreur de l’API :", error.response.data);
                } else {
                    console.error("Une erreur inattendue est survenue :", error);
                }
            }
        };
        fetchProfileData();
    }, [apiClient]);


    useEffect(() => {
        const fetchCountries = async () => {
            try {
                const response = await apiClient.GetAllCountriesEndpoint();
                response.sort((a: Country, b: Country) => a.name.localeCompare(b.name));
                setCountries(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    console.error("Erreur de l’API :", error.response.data);
                } else {
                    console.error("Une erreur inattendue est survenue :", error);
                }
            }
        };
        fetchCountries();
    }, [apiClient]);

    useEffect(() => {
        const fetchLanguages = async () => {
            try {
                const response = await apiClient.GetAllLanguagesEndpoint();
                response.sort((a: TranslationLanguage, b: TranslationLanguage) => a.name.localeCompare(b.name));
                setLanguages(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    console.error("Erreur de l’API :", error.response.data);
                } else {
                    console.error("Une erreur inattendue est survenue :", error);
                }
            }
        };
        fetchLanguages();
    }, [apiClient]);

    useEffect(() => {
        const fetchServices = async () => {
            try {
                const response = await apiClient.GetAllServicesEndpoint();
                response.sort((a: Service, b: Service) => a.name.localeCompare(b.name));
                setServices(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    console.error("Erreur de l’API :", error.response.data);
                } else {
                    console.error("Une erreur inattendue est survenue :", error);
                }
            }
        };
        fetchServices();
    }, [apiClient]);

    function handleModifyPersonalData() {
        setSavedProfileData(profileData);
        setEditingForm("personal");
    }

    function handleCancelPersonalData() {
        setProfileData(savedProfileData);
        setEditingForm(null);
    }

    async function handleSubmitPersonalData(e: React.SyntheticEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const personalData: PersonalData = {
            firstName: formData.get("firstName") as string,
            lastName: formData.get("lastName") as string,
            email: formData.get("email") as string,
            phone: formData.get("phone") as string | null
        };

        const validationResult = PersonalDataSchema.safeParse(personalData);
        if (!validationResult.success) {
            setFieldErrors(zod.flattenError(validationResult.error).fieldErrors);
            return;
        }

        setFieldErrors({});

        try {
            await apiClient.UpdateFreelancePersonalDataEndpoint({ body: { ...personalData }, pathParams: { freelanceId: profileData?.id || 0 } });
            setEditingForm(null);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                setFieldErrors(error.response.data.errors || {});
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    }

    function handleModifyBusinessData() {
        setSavedProfileData(profileData);
        setProfileData(prev => {
            if (!prev || prev.address) return prev;
            return { ...prev, address: { streetInfo: "", addressComplement: null, postCode: "", city: "", state: null, countryId: 0 } };
        });
        setEditingForm("business");
    }

    function handleCancelBusinessData() {
        setProfileData(savedProfileData);
        setEditingForm(null);
    }

    function handleSubmitBusinessData(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        console.log("Submitted business data:", {
            streetInfo: profileData?.address?.streetInfo,
            addressComplement: profileData?.address?.addressComplement,
            postCode: profileData?.address?.postCode,
            city: profileData?.address?.city,
            state: profileData?.address?.state,
            countryId: profileData?.address?.countryId
        });
    }

    function handleModifyLanguages() {
        setSavedProfileData(profileData);
        setEditingForm("languages");
    }

    function handleCancelLanguages() {
        setProfileData(savedProfileData);
        setEditingForm(null);
    }

    function handleSubmitLanguages(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        console.log("Submitted languages data:", {
            sourceLanguages: profileData?.sourceLanguages,
            targetLanguages: profileData?.targetLanguages
        });
    }

    function handleModifyService() {
        setSavedProfileData(profileData);
        setEditingForm("services");
    }

    function handleCancelService() {
        setProfileData(savedProfileData);
        setEditingForm(null);
    }

    function handleSubmitServices(event: React.SyntheticEvent<HTMLFormElement>) {
        event.preventDefault();
        console.log("Submitted services data:", {
            services: profileData?.services
        });
    }


    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Paramètres du profil" pageSubtitle="Gérez vos informations personnelles et commerciales"></PageHeader>

                {profileData ? (<>

                    <FormContainer
                        icon={<Profile />}
                        title="Informations personnelles"
                        presentation="Informations générales sur votre compte"
                        cancel_button_name="Annuler"
                        save_button_name="Enregistrer"
                        modify_button_name="Modifier"
                        isEditing={editingForm === "personal"}
                        modifyDisabled={editingForm !== null}
                        onModify={handleModifyPersonalData}
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
                        isEditing={editingForm === "business"}
                        modifyDisabled={editingForm !== null}
                        onModify={handleModifyBusinessData}
                        onCancel={handleCancelBusinessData}
                        onSubmit={handleSubmitBusinessData}>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Adresse"
                                name="streetInfo"
                                type="text"
                                placeholder="12 avenue des Champs-Élysées"
                                value={profileData.address?.streetInfo || ""}
                                readonly={editingForm !== "business"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, streetInfo: value } : null } : prev)}
                                error={fieldErrors.streetInfo?.[0]}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Complément d’adresse"
                                name="addressComplement"
                                type="text"
                                placeholder="Bâtiment A"
                                value={profileData.address?.addressComplement || ""}
                                readonly={editingForm !== "business"}
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
                                readonly={editingForm !== "business"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, postCode: value } : null } : prev)}
                                error={fieldErrors.postCode?.[0]}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Ville"
                                name="city"
                                type="text"
                                placeholder="Paris"
                                value={profileData.address?.city || ""}
                                readonly={editingForm !== "business"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, city: value } : null } : prev)}
                                error={fieldErrors.city?.[0]}>
                            </FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="État/Région"
                                name="state"
                                type="text"
                                required={false}
                                placeholder="Île-de-France"
                                value={profileData.address?.state || ""}
                                readonly={editingForm !== "business"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, state: value } : null } : prev)}
                                error={fieldErrors.state?.[0]}>
                            </FormInputGroup>
                            <FormSelectGroup
                                label="Pays"
                                name="countryId"
                                options={countries.map(country => ({ value: country.id.toString(), name: country.name }))}
                                placeholder="-- Sélectionnez le pays --"
                                selected={profileData.address?.countryId ? profileData.address.countryId.toString() : ""}
                                disabled={editingForm !== "business"}
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
                            onCancel={handleCancelLanguages}
                            onSubmit={handleSubmitLanguages}>
                            <div className="language_container">
                                <div className="source_language_container">
                                    <h3 className="language_title">Langues source</h3>
                                    <p></p>
                                    <div className="language_list">{
                                        languages.map(language => (
                                            CheckboxOption({
                                                name: `source-language-${language.id}`,
                                                label: language.name,
                                                checked: profileData.sourceLanguages.some(l => l.id === language.id),
                                                disabled: editingForm !== "languages",
                                                onChange: () => {
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
                                                }
                                            })
                                        ))
                                    }</div>
                                </div>
                                <div className="target_language_container">
                                    <h3 className="language_title">Langues cible</h3>
                                    <p></p>
                                    <div className="language_list">{
                                        languages.map(language => (
                                            CheckboxOption({
                                                name: `target-language-${language.id}`,
                                                label: language.name,
                                                checked: profileData.targetLanguages.some(l => l.id === language.id),
                                                disabled: editingForm !== "languages",
                                                onChange: () => {
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
                                                }
                                            })
                                        ))
                                    }</div>
                                </div>
                            </div>
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
                            onCancel={handleCancelService}
                            onSubmit={handleSubmitServices}>
                            <div className="service_list">{
                                services.map(service => (
                                    CheckboxOption({
                                        name: `service-${service.id}`,
                                        label: service.name,
                                        checked: profileData.services.some(s => s.id === service.id),
                                        disabled: editingForm !== "services",
                                        onChange: () => {
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
                                        }
                                    })
                                ))
                            }</div>
                        </FormContainer>
                    </section>
                </>
                ) : (
                    <p>Chargement des données du profil…</p>
                )}
            </AppLayout>
        </>
    );
}

export default ProfileView;