import "./BurgerMenu.css";
import { BurgerIcon } from "../../assets/icons/icons";
import { useState } from "react";
import { Link } from "react-router";
import Button from "../../components/ui/Button";

function BurgerMenu() {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <div className="burger_menu">
            <button
                aria-label={isOpen ? "Fermer le menu" : "Ouvrir le menu"}
                className="burger_toggle"
                onClick={() => setIsOpen(!isOpen)}
            >
                {isOpen ? <BurgerIcon className="burger_icon" /> : <BurgerIcon className="burger_icon" />}
            </button>

            {isOpen && (
                <div className="burger_dropdown">
                    <Link to={"/login"} className="button_link"><Button name="Connexion" width="medium" variant="blue" /></Link>
                    <Link to={"/signup"} className="button_link"><Button name="Inscription" width="medium" variant="blue" /></Link>
                </div>
            )}
        </div>
    );
}

export default BurgerMenu;