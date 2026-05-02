import AppLayout from "../components/ui/AppLayout";
import Button from "../components/ui/Button";
import PageHeader from "../components/ui/PageHeader";

function ProfileView() {
    return (
        <>
            <AppLayout>
                <PageHeader pageTitle="Paramètres du profil" pageSubtitle="Gérez vos informations personnelles et commerciales" button={<Button variant="dark" name="Modifier" width="medium" type="button"></Button>}></PageHeader>
            </AppLayout>
        </>
    );
}

export default ProfileView;