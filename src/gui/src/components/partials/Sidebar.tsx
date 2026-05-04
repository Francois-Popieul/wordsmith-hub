import { useNavigate, useLocation } from "react-router";
import Button from "../ui/Button";
import "./Sidebar.css";
import Brand from "./Brand";
import { Dashboard } from "../../assets/icons/icons.ts";
import { Customers } from "../../assets/icons/icons.ts";
import { Projects } from "../../assets/icons/icons.ts";
import { Orders } from "../../assets/icons/icons.ts";
import { Invoices } from "../../assets/icons/icons.ts";
import { Profile } from "../../assets/icons/icons.ts";
import { Logout } from "../../assets/icons/icons.ts";

function Sidebar() {
    const navigate = useNavigate();
    const location = useLocation();

    const handleNav = (route: string) => {
        navigate(route);
    };

    const variant = (route: string) => location.pathname === route ? "sidebar_selected" : "sidebar";

    return (
        <div className="sidebar">
            <div className="sidebar_inner_container">
                <Brand variant="light" width="small" />
                <hr />
                <Button name="Tableau de bord" variant={variant("/dashboard")} width="full_width" onClick={() => handleNav("/dashboard")}><Dashboard /></Button>
                <Button name="Clients" variant={variant("/direct-customers")} width="full_width" onClick={() => handleNav("/direct-customers")}><Customers /></Button>
                <Button name="Projets" variant={variant("/projects")} width="full_width" onClick={() => handleNav("/projects")}><Projects /></Button>
                <Button name="Commandes" variant={variant("/orders")} width="full_width" onClick={() => handleNav("/orders")}><Orders /></Button>
                <Button name="Factures" variant={variant("/invoices")} width="full_width" onClick={() => handleNav("/invoices")}><Invoices /></Button>
                <Button name="Profil" variant={variant("/profile")} width="full_width" onClick={() => handleNav("/profile")}><Profile /></Button>
            </div>
            <div className="sidebar_inner_container">
                <hr />
                <Button name="Se déconnecter" variant="sidebar" width="full_width" onClick={() => handleNav("/logout")}><Logout /></Button>
            </div>
        </div>
    );
}

export default Sidebar;