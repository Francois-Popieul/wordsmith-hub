import "./BurgerMenu.css";
import { BurgerIcon } from "../../assets/icons/icons";
import { useState } from "react";
import { useNavigate } from "react-router";
import Button from "../../components/ui/Button";

function BurgerMenu() {
    const [isOpen, setIsOpen] = useState(false);
    const navigate = useNavigate();

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
                    <Button name="Connexion" width="medium" variant="blue" onClick={() => navigate("/login")} />
                    <Button name="Inscription" width="medium" variant="blue" onClick={() => navigate("/signup")} />
                </div>
            )}
        </div>
    );
}

export default BurgerMenu;