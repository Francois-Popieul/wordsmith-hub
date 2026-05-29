import { useEffect, useState } from "react";
import * as zod from "zod";
import axios from "axios";
import { schemas } from "../infrastructure/openApi/client";
import { useToast } from "./useToast";
import { useApiClient } from "./useApiClient";

export function useCurrencies(): zod.infer<typeof schemas.Currency>[] {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [currencies, setCurrencies] = useState<zod.infer<typeof schemas.Currency>[]>([]);

    useEffect(() => {
        if (!token) return;
        const fetchCurrencies = async () => {
            try {
                const response = await apiClient.GetAllCurrenciesEndpoint();
                response.sort((a, b) => a.name.localeCompare(b.name));
                setCurrencies(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement de la liste des devises.", "top_right", 3000);
                }
            }
        };
        fetchCurrencies();
    }, [apiClient, addToast, token]);

    return currencies;
}

export function useCountries(): zod.infer<typeof schemas.Country>[] {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [countries, setCountries] = useState<zod.infer<typeof schemas.Country>[]>([]);

    useEffect(() => {
        if (!token) return;
        const fetchCountries = async () => {
            try {
                const response = await apiClient.GetAllCountriesEndpoint();
                response.sort((a, b) => a.name.localeCompare(b.name));
                setCountries(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement de la liste des pays.", "top_right", 3000);
                }
            }
        };
        fetchCountries();
    }, [apiClient, addToast, token]);

    return countries;
}

export function useLanguages(): zod.infer<typeof schemas.TranslationLanguage>[] {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [languages, setLanguages] = useState<zod.infer<typeof schemas.TranslationLanguage>[]>([]);

    useEffect(() => {
        if (!token) return;
        const fetchLanguages = async () => {
            try {
                const response = await apiClient.GetAllLanguagesEndpoint();
                response.sort((a, b) => a.name.localeCompare(b.name));
                setLanguages(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement de la liste des langues.", "top_right", 3000);
                }
            }
        };
        fetchLanguages();
    }, [apiClient, addToast, token]);

    return languages;
}

export function useServices(): zod.infer<typeof schemas.Service>[] {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [services, setServices] = useState<zod.infer<typeof schemas.Service>[]>([]);

    useEffect(() => {
        if (!token) return;
        const fetchServices = async () => {
            try {
                const response = await apiClient.GetAllServicesEndpoint();
                response.sort((a, b) => a.name.localeCompare(b.name));
                setServices(response);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l'API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s'est produite lors du chargement de la liste des services.", "top_right", 3000);
                }
            }
        };
        fetchServices();
    }, [apiClient, addToast, token]);

    return services;
}
