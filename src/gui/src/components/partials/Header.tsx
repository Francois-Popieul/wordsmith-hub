import { Link } from "react-router";
import Button from "../ui/Button";
import "./Header.css";
import Brand from "./Brand";
import BurgerMenu from "../../homepage/components/BurgerMenu";

function Header() {
    return (
        <header className="homepage_header">
            <Brand variant="dark" width="large" />
            <nav className="navigation_container">
                <div className="button_container">
                    <Link to="/login" className="button_link"><Button name="Se connecter" variant="light" width="medium" /></Link>
                    <Link to="/signup" className="button_link"><Button name="Commencer" variant="blue" width="medium" /></Link>
                </div>
                <BurgerMenu />
            </nav>
        </header>
    );
}

export default Header;