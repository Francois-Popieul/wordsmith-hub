import { Link } from "react-router";
import Button from "../ui/Button";
import "./Sidebar.css";
import Brand from "./Brand";

function Sidebar() {
    return (
        <div className="sidebar">
            <Brand variant="light" width="small" />
            <div>
                <Link to={"/dashboard"}><Button name="Tableau de bord" variant="sidebar_selected" width="full_width" ></Button></Link>
                <Link to={"/directCustomers"}><Button name="Clients" variant="sidebar" width="full_width" ></Button></Link>
                <Link to={"/orders"}><Button name="Commandes" variant="sidebar" width="full_width" ></Button></Link>
                <Link to={"/invoices"}><Button name="Factures" variant="sidebar" width="full_width" ></Button></Link>
                <Link to={"/profile"}><Button name="Profil" variant="sidebar" width="full_width" ></Button></Link>
            </div>
        </div>
    );
}

export default Sidebar;