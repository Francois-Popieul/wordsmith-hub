import { useEffect, useMemo, useState } from "react";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import { createApiClient } from "../../infrastructure/openApi/client";
import axios from "axios";
import FreelanceDto from "../models/FreelanceDto";
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

function ProfileView() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);

    const [profileData, setProfileData] = useState<FreelanceDto | void>();
    const [savedProfileData, setSavedProfileData] = useState<FreelanceDto | void>();
    const [countries, setCountries] = useState<Country[]>([]);
    const [languages, setLanguages] = useState<TranslationLanguage[]>([]);
    const [services, setServices] = useState<Service[]>([]);
    const [editingForm, setEditingForm] = useState<string | null>(null);

    useEffect(() => {
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                console.log("Profile data:", response);
                const freelancceData = new FreelanceDto(response.id, response.firstName, response.lastName, response.email, response.phone, response.address, response.statusId);
                setProfileData(freelancceData);
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
                console.log("Countries data:", response);
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
                console.log("Languages data:", response);
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
                console.log("Services data:", response);
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

    function handleSubmitPersonalData(e: React.SyntheticEvent<HTMLFormElement>) {
        e.preventDefault();
        console.log("Submitted personal data:", {
            firstName: profileData?.firstName,
            lastName: profileData?.lastName,
            email: profileData?.email,
            phone: profileData?.phone
        });
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
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, firstName: value } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Nom"
                                name="lastName"
                                type="text"
                                placeholder="Dupont"
                                value={profileData.lastName}
                                readonly={editingForm !== "personal"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, lastName: value } : prev)}>
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
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, email: value } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Téléphone"
                                name="phone"
                                type="tel"
                                placeholder="0123456789"
                                value={profileData.phone || ""}
                                readonly={editingForm !== "personal"}
                                required={false}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, phone: value } : prev)}>
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
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, streetInfo: value } : null } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Complément d’adresse"
                                name="addressComplement"
                                type="text"
                                placeholder="Bâtiment A"
                                value={profileData.address?.addressComplement || ""}
                                readonly={editingForm !== "business"}
                                required={false}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, addressComplement: value } : null } : prev)}>
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
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, postCode: value } : null } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Ville"
                                name="city"
                                type="text"
                                placeholder="Paris"
                                value={profileData.address?.city || ""}
                                readonly={editingForm !== "business"}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, city: value } : null } : prev)}>
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
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, state: value } : null } : prev)}>
                            </FormInputGroup>
                            <FormSelectGroup
                                label="Pays"
                                name="countryId"
                                options={countries.sort((a, b) => a.name.localeCompare(b.name)).map(country => ({ value: country.id.toString(), name: country.name }))}
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

                    <FormContainer
                        icon={<LanguageIcon />}
                        title="Compétences linguistiques"
                        presentation="Langues de travail que vous utilisez"
                        cancel_button_name="Annuler"
                        save_button_name="Enregistrer"
                        modify_button_name="Modifier"
                        isEditing={false}
                        onModify={() => null}
                        onCancel={() => null}
                        onSubmit={() => null}>
                        <div className="language_container">
                            <div className="source_language_container">
                                <p className="language_title">Langues source</p>
                                <p></p>
                            </div>
                            <div className="target_language_container">
                                <p className="language_title">Langues cible</p>
                                <p></p>
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
                        isEditing={false}
                        onModify={() => null}
                        onCancel={() => null}
                        onSubmit={() => null}>
                        <div>Empty</div>
                    </FormContainer>

                </>
                ) : (
                    <p>Chargement des données du profil…</p>
                )}
            </AppLayout>
        </>
    );
}

export default ProfileView;