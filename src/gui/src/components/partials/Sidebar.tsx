import { useNavigate, useLocation } from "react-router";
import Button from "../ui/Button";
import "./Sidebar.css";
import Brand from "./Brand";
import { MdOutlineDashboard } from "react-icons/md";
import { MdOutlineGroup } from "react-icons/md";
import { IoFolderOpenOutline } from "react-icons/io5";
import { FaRegFileAlt } from "react-icons/fa";
import { LiaFileInvoiceDollarSolid } from "react-icons/lia";
import { IoPersonOutline } from "react-icons/io5";
import { MdLogout } from "react-icons/md";

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
                <Button name="Tableau de bord" variant={variant("/dashboard")} width="full_width" onClick={() => handleNav("/dashboard")}><MdOutlineDashboard /></Button>
                <Button name="Clients" variant={variant("/direct-customers")} width="full_width" onClick={() => handleNav("/direct-customers")}><MdOutlineGroup /></Button>
                <Button name="Projets" variant={variant("/projects")} width="full_width" onClick={() => handleNav("/projects")}><IoFolderOpenOutline /></Button>
                <Button name="Commandes" variant={variant("/orders")} width="full_width" onClick={() => handleNav("/orders")}><FaRegFileAlt /></Button>
                <Button name="Factures" variant={variant("/invoices")} width="full_width" onClick={() => handleNav("/invoices")}><LiaFileInvoiceDollarSolid /></Button>
                <Button name="Profil" variant={variant("/profile")} width="full_width" onClick={() => handleNav("/profile")}><IoPersonOutline /></Button>
            </div>
            <div className="sidebar_inner_container">
                <hr />
                <Button name="Se déconnecter" variant="sidebar" width="full_width" onClick={() => handleNav("/logout")}><MdLogout /></Button>
            </div>
        </div>
    );
}

export default Sidebar;