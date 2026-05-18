import { Navigate } from "react-router";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";

function DashboardView() {
    const token = localStorage.getItem("wshToken");

    if (!token) {
        return <Navigate to="/" />;
    }

    return (
        <AppLayout>
            <PageHeader pageTitle="Tableau de bord" pageSubtitle="Bienvenue ! Retrouvez ici un résumé de votre activité."></PageHeader>
        </AppLayout>

    );
}

export default DashboardView;
