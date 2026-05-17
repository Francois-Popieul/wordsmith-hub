import { useNavigate, useLocation } from "react-router";
import Button from "../ui/Button";
import "./Sidebar.css";
import Brand from "./Brand";
import { CustomersIcon, DashboardIcon, InvoicesIcon, LogoutIcon, OrdersIcon, ProfileIcon, ProjectsIcon } from "../../assets/icons/icons.ts";

function Sidebar() {
    const navigate = useNavigate();
    const location = useLocation();

    const handleNav = (route: string) => {
        navigate(route);
    };

    const variant = (route: string) => location.pathname === route ? "sidebar_selected" : "sidebar";

    function handleLogout() {
        localStorage.removeItem("wshToken");
        navigate("/");
    }

    return (
        <div className="sidebar">
            <div className="sidebar_inner_container">
                <Brand variant="light" width="small" />
                <hr />
                <Button name="Tableau de bord" variant={variant("/dashboard")} width="full_width" onClick={() => handleNav("/dashboard")}><DashboardIcon /></Button>
                <Button name="Clients" variant={variant("/direct-customers")} width="full_width" onClick={() => handleNav("/direct-customers")}><CustomersIcon /></Button>
                <Button name="Projets" variant={variant("/projects")} width="full_width" onClick={() => handleNav("/projects")}><ProjectsIcon /></Button>
                <Button name="Commandes" variant={variant("/orders")} width="full_width" onClick={() => handleNav("/orders")}><OrdersIcon /></Button>
                <Button name="Factures" variant={variant("/invoices")} width="full_width" onClick={() => handleNav("/invoices")}><InvoicesIcon /></Button>
                <Button name="Profil" variant={variant("/profile")} width="full_width" onClick={() => handleNav("/profile")}><ProfileIcon /></Button>
            </div>
            <div className="sidebar_inner_container">
                <hr />
                <Button name="Se déconnecter" variant="sidebar" width="full_width" onClick={handleLogout}><LogoutIcon /></Button>
            </div>
        </div>
    );
}

export default Sidebar;