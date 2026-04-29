import { Link } from "react-router";
import Button from "../ui/Button";
import "./Header.css";
import Brand from "./Brand";
import BurgerMenu from "../ui/BurgerMenu";

function Header() {
    return (
        <header className="homepage_header">
            <Brand variant="dark" width="large" />
            <nav className="navigation_container">
                <div className="button_container">
                    <Link to="/login"><Button name="Se connecter" variant="light" width="medium" /></Link>
                    <Link to="/signup"><Button name="Commencer" variant="dark" width="medium" /></Link>
                </div>
                <BurgerMenu />
            </nav>
        </header>
    );
}

export default Header;