import { useEffect, useState } from "react";
import { useApiClient } from "./useApiClient";
import { useToast } from "./useToast";
import axios from "axios";

export function useDirectCustomerCount(): number {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [count, setCount] = useState<number>(0);

    useEffect(() => {
        if (!token) return;
        const fetchDirectCustomerCount = async () => {
            try {
                const response = await apiClient.GetAllDirectCustomersEndpoint();
                setCount(response.length);
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
        fetchDirectCustomerCount();
    }, [apiClient, addToast, token]);

    return count;
}

export function useProjectCount(): number {
    const { token, apiClient } = useApiClient();
    const { addToast } = useToast();
    const [count, setCount] = useState<number>(0);

    useEffect(() => {
        if (!token) return;
        const fetchProjectCount = async () => {
            try {
                const response = await apiClient.GetAllProjectsEndpoint();
                setCount(response.length);
            } catch (error) {
                if (axios.isAxiosError(error) && error.response) {
                    const data = error.response.data;
                    const message = typeof data === "string" ? data : (data?.message ?? JSON.stringify(data));
                    addToast("error", `Erreur de l’API : ${message}`, "top_right", 3000);
                } else {
                    addToast("error", "Une erreur inattendue s’est produite lors du chargement de la liste des projets.", "top_right", 3000);
                }
            }
        };
        fetchProjectCount();
    }, [apiClient, addToast, token]);

    return count;
}
