import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { createApiClient } from "../../infrastructure/openApi/client";
import AppLayout from "../../components/ui/AppLayout";

function DashboardView() {
    const token = localStorage.getItem("wshToken");
    const apiClient = useMemo(() => createApiClient(import.meta.env.VITE_API_BASE_URL, {
        axiosConfig: token ? { headers: { Authorization: `Bearer ${token}` } } : undefined
    }), [token]);

    const [dashboardData, setDashboardData] = useState<object | void>();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await apiClient.GetFreelanceEndpoint();
                console.log("Dashboard data:", response);
                setDashboardData(response);
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
        <AppLayout>
            <h1>{dashboardData ? "Dashboard" : "Chargement en cours…"}</h1>
        </AppLayout>

    );
}

export default DashboardView;