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

function ProfileView() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);

    const [profileData, setProfileData] = useState<FreelanceDto | void>();

    useEffect(() => {
        const fetchData = async () => {
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
        fetchData();
    }, [apiClient]);

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Paramètres du profil" pageSubtitle="Gérez vos informations personnelles et commerciales"></PageHeader>
                {profileData ? (<>
                    <FormContainer icon={<Profile />} title="Informations personnelles" presentation="Informations générales sur votre compte" button_name="Modifier" onSubmit={() => null}>
                        <div className="form_inner_flex_container">
                            <FormInputGroup label="Prénom" name="firstName" type="text" placeholder="Jean" value={profileData.firstName} readonly={true}></FormInputGroup>
                            <FormInputGroup label="Nom" name="lastName" type="text" placeholder="Dupont" value={profileData.lastName} readonly={true}></FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup label="E-mail" name="email" type="email" placeholder="jean.dupont@example.com" value={profileData.email} readonly={true}></FormInputGroup>
                            <FormInputGroup label="Téléphone" name="phone" type="tel" placeholder="0123456789" value={profileData.phone || ""} readonly={true}></FormInputGroup>
                        </div>
                    </FormContainer>
                    <FormContainer icon={<BuildingIcon />} title="Adresse de facturation" presentation="Détails de votre adresse de facturation" button_name="Modifier" onSubmit={() => null}>
                        <div className="form_inner_flex_container">
                            <FormInputGroup label="Adresse" name="streetInfo" type="text" placeholder="123 Rue Exemple" value={profileData.address?.streetInfo || ""} readonly={true}></FormInputGroup>
                        </div>
                        <div className="form_inner_flex_container">
                            <FormInputGroup label="Ville" name="city" type="text" placeholder="Paris" value={profileData.address?.city || ""} readonly={true}></FormInputGroup>
                            <FormInputGroup label="Code Postal" name="postCode" type="text" placeholder="75001" value={profileData.address?.postCode || ""} readonly={true}></FormInputGroup>
                            <FormInputGroup label="Pays" name="countryId" type="text" placeholder="France" value={profileData.address?.countryId?.toString() || ""} readonly={true}></FormInputGroup>
                        </div>
                    </FormContainer>
                    <FormContainer icon={<LanguageIcon />} title="Compétences linguistiques" presentation="Langues de travail que vous utilisez" button_name="Modifier" onSubmit={() => null}>
                        <div>Empty</div>
                    </FormContainer>
                    <FormContainer icon={<BriefcaseIcon />} title="Services" presentation="Services que vous proposez" button_name="Modifier" onSubmit={() => null}>
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