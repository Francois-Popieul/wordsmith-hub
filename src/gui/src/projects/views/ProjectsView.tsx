import { FaPlus } from "react-icons/fa6";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";

function ProjectsView() {
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Projets" pageSubtitle="Gérez vos projets de traduction" button={<Button variant="dark" name="Ajouter un projet" width="default" type="button"><FaPlus /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default ProjectsView;