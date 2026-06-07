import { useMemo } from "react";
import { createApiClient } from "../infrastructure/openApi/client";
import { useAuth } from "./useAuth/useAuth";

export function useApiClient() {
    const token = useAuth().token;
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);
    return { token, apiClient };
}