import { useMemo } from "react";
import { createApiClient } from "../infrastructure/openApi/client";

export function useApiClient() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    return { token, apiClient };
}