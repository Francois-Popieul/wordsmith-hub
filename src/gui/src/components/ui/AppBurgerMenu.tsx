import "./AppBurgerMenu.css";
import { BurgerIcon, CustomersIcon, DashboardIcon, InvoicesIcon, LogoutIcon, OrdersIcon, ProfileIcon, ProjectsIcon } from "../../assets/icons/icons";
import { useState } from "react";
import { useNavigate } from "react-router";
import Button from "../../components/ui/Button";

function AppBurgerMenu() {
    const navigate = useNavigate();
    const handleNav = (route: string) => { navigate(route); };

    const [isOpen, setIsOpen] = useState(false);

    function handleLogout() {
        localStorage.removeItem("wshToken");
        navigate("/");
    }

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
                    <Button name="Accueil" variant="blue" width="medium" onClick={() => handleNav("/dashboard")}><DashboardIcon /></Button>
                    <Button name="Clients" variant="blue" width="medium" onClick={() => handleNav("/direct-customers")}><CustomersIcon /></Button>
                    <Button name="Projets" variant="blue" width="medium" onClick={() => handleNav("/projects")}><ProjectsIcon /></Button>
                    <Button name="Commandes" variant="blue" width="medium" onClick={() => handleNav("/orders")}><OrdersIcon /></Button>
                    <Button name="Factures" variant="blue" width="medium" onClick={() => handleNav("/invoices")}><InvoicesIcon /></Button>
                    <Button name="Profil" variant="blue" width="medium" onClick={() => handleNav("/profile")}><ProfileIcon /></Button>
                    <Button name="Déconnexion" variant="blue" width="medium" onClick={handleLogout}><LogoutIcon /></Button>
                </div>
            )}
        </div>
    );
}

export default AppBurgerMenu;