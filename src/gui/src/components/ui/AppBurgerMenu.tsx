import "./AppBurgerMenu.css";
import { BurgerIcon } from "../../assets/icons/icons";
import { useState } from "react";
import { useNavigate } from "react-router";
import Button from "../../components/ui/Button";
import { Dashboard } from "../../assets/icons/icons.ts";
import { Customers } from "../../assets/icons/icons.ts";
import { Projects } from "../../assets/icons/icons.ts";
import { Orders } from "../../assets/icons/icons.ts";
import { Invoices } from "../../assets/icons/icons.ts";
import { Profile } from "../../assets/icons/icons.ts";
import { Logout } from "../../assets/icons/icons.ts";

function AppBurgerMenu() {
    const navigate = useNavigate();
    const handleNav = (route: string) => { navigate(route); };

    const [isOpen, setIsOpen] = useState(false);

    return (
        <div className="app_burger_menu">
            <button
                aria-label={isOpen ? "Fermer le menu" : "Ouvrir le menu"}
                className="app_burger_toggle"
                onClick={() => setIsOpen(!isOpen)}
            >
                {isOpen ? <BurgerIcon className="app_burger_icon" /> : <BurgerIcon className="app_burger_icon" />}
            </button>

            {isOpen && (
                <div className="app_burger_dropdown">
                    <Button name="Accueil" variant="dark" width="medium" onClick={() => handleNav("/dashboard")}><Dashboard /></Button>
                    <Button name="Clients" variant="dark" width="medium" onClick={() => handleNav("/direct-customers")}><Customers /></Button>
                    <Button name="Projets" variant="dark" width="medium" onClick={() => handleNav("/projects")}><Projects /></Button>
                    <Button name="Commandes" variant="dark" width="medium" onClick={() => handleNav("/orders")}><Orders /></Button>
                    <Button name="Factures" variant="dark" width="medium" onClick={() => handleNav("/invoices")}><Invoices /></Button>
                    <Button name="Profil" variant="dark" width="medium" onClick={() => handleNav("/profile")}><Profile /></Button>
                    <Button name="Déconnexion" variant="dark" width="medium" onClick={() => handleNav("/logout")}><Logout /></Button>
                </div>
            )}
        </div>
    );
}

export default AppBurgerMenu;