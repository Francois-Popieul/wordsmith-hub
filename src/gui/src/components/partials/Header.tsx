import { useNavigate } from "react-router";
import Button from "../ui/Button";
import "./Header.css";
import Brand from "./Brand";
import BurgerMenu from "../../homepage/components/BurgerMenu";

function Header() {
    const navigate = useNavigate();

    return (
        <header className="homepage_header">
            <Brand variant="dark" width="large" />
            <nav className="navigation_container">
                <div className="button_container">
                    <Button name="Se connecter" variant="light" width="medium" onClick={() => navigate("/login")} />
                    <Button name="Commencer" variant="blue" width="medium" onClick={() => navigate("/signup")} />
                </div>
                <BurgerMenu />
            </nav>
        </header>
    );
}

export default Header;