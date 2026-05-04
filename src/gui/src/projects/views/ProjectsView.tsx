import { PlusSign } from "../../assets/icons/icons";
import AppLayout from "../../components/ui/AppLayout";
import PageHeader from "../../components/ui/PageHeader";
import Button from "../../components/ui/Button";

function ProjectsView() {
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Projets" pageSubtitle="Gérez vos projets de traduction" button={<Button variant="dark" name="Ajouter un projet" width="default" type="button"><PlusSign /></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default ProjectsView;