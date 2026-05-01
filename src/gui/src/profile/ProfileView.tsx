import Sidebar from "../components/partials/Sidebar";

function ProfileView() {
    return (
        <>
            <div className="profile">
                <h1>User Profile</h1>
                <p>This is the profile page where you can view and edit your personal information.</p>
            </div>
            <Sidebar />
        </>
    );
}

export default ProfileView;