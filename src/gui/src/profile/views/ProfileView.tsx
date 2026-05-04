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

function ProfileView() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);

    const [profileData, setProfileData] = useState<FreelanceDto | void>();
    const [savedProfileData, setSavedProfileData] = useState<FreelanceDto | void>();
    const [countries, setCountries] = useState<{ id: number, code: string; name: string, isEuropeanUnionMember: boolean }[]>([]);
    const [isEditingPersonalData, setIsEditingPersonalData] = useState(false);
    const [isEditingBusinessData, setIsEditingBusinessData] = useState(false);

    useEffect(() => {
        const fetchProfileData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                console.log("Profile data:", response);
                const freelancceData = new FreelanceDto(response.id, response.firstName, response.lastName, response.email, response.phone, response.address, response.statusId);
                setProfileData(freelancceData);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    console.error("API error:", error.response.data);
                } else {
                    console.error("An unexpected error occurred:", error);
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
                    console.error("API error:", error.response.data);
                } else {
                    console.error("An unexpected error occurred:", error);
                }
            }
        };
        fetchCountries();
    }, [apiClient]);

    function handleModifyPersonalData() {
        setSavedProfileData(profileData);
        setIsEditingPersonalData(true);
    }

    function handleCancelPersonalData() {
        setProfileData(savedProfileData);
        setIsEditingPersonalData(false);
    }

    function handleSubmitBusinessData(e: React.SyntheticEvent<HTMLFormElement>) {
        e.preventDefault();
    }

    function handleModifyBusinessData() {
        setSavedProfileData(profileData);
        setProfileData(prev => {
            if (!prev || prev.address) return prev;
            return { ...prev, address: { streetInfo: "", addressComplement: null, postCode: "", city: "", state: null, countryId: 0 } };
        });
        setIsEditingBusinessData(true);
    }

    function handleCancelBusinessData() {
        setProfileData(savedProfileData);
        setIsEditingBusinessData(false);
    }

    function handleSubmitPersonalData(e: React.SyntheticEvent<HTMLFormElement>) {
        e.preventDefault();
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
                        isEditing={isEditingPersonalData}
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
                                readonly={!isEditingPersonalData}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, firstName: value } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Nom"
                                name="lastName"
                                type="text"
                                placeholder="Dupont"
                                value={profileData.lastName}
                                readonly={!isEditingPersonalData}
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
                                readonly={!isEditingPersonalData}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, email: value } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Téléphone"
                                name="phone"
                                type="tel"
                                placeholder="0123456789"
                                value={profileData.phone || ""}
                                readonly={!isEditingPersonalData}
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
                        isEditing={isEditingBusinessData}
                        onModify={handleModifyBusinessData}
                        onCancel={handleCancelBusinessData}
                        onSubmit={handleSubmitBusinessData}>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Adresse"
                                name="streetInfo"
                                type="text"
                                placeholder="123 Rue Exemple"
                                value={profileData.address?.streetInfo || ""}
                                readonly={!isEditingBusinessData}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, streetInfo: value } : null } : prev)}>
                            </FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup
                                label="Ville"
                                name="city"
                                type="text"
                                placeholder="Paris"
                                value={profileData.address?.city || ""}
                                readonly={!isEditingBusinessData}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, city: value } : null } : prev)}>
                            </FormInputGroup>
                            <FormInputGroup
                                label="Code postal"
                                name="postCode"
                                type="text"
                                placeholder="75001"
                                value={profileData.address?.postCode || ""}
                                readonly={!isEditingBusinessData}
                                onChange={(value) => setProfileData(prev => prev ? { ...prev, address: prev.address ? { ...prev.address, postCode: value } : null } : prev)}>
                            </FormInputGroup>
                            <FormSelectGroup
                                label="Pays"
                                name="countryId"
                                options={[
                                    { value: "country-invite", name: "-- Sélectionnez le pays --" },
                                    ...countries.sort((a, b) => a.name.localeCompare(b.name)).map(country => ({ value: country.id.toString(), name: country.name }))]}
                                selected={profileData.address?.countryId?.toString() || ""}
                                readonly={!isEditingBusinessData}
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
                        <div>Empty</div>
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