import { PlusSignIcon } from "../../assets/icons/icons";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";
import { Navigate } from "react-router";

function ProjectsView() {
    const token = localStorage.getItem("wshToken");

    if (!token) {
        return <Navigate to="/" />;
    }

    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Projets" pageSubtitle="Gérez vos projets de traduction" button={<Button variant="blue" name="Ajouter un projet" width="default" type="button"><PlusSignIcon /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default ProjectsView;