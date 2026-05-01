import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import { createApiClient } from "../../infrastructure/openApi/client";
import Sidebar from "../../components/partials/Sidebar";

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
        <div className="dashboard">
            <h1>{dashboardData ? "Dashboard" : "Loading..."}</h1>
            <p>{JSON.stringify(dashboardData)}</p>
            <Sidebar />
        </div>
    );
}

export default DashboardView;