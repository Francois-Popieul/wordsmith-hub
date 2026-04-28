import { Link } from "react-router";
import Button from "../ui/Button";
import "./Header.css";
import Brand from "./Brand";

function Header() {
    return (
        <header className="homepage_header">
            <Brand variant="dark" width="large" />
            <nav className="navigation_container">
                <Link to="/login"><Button name="Se connecter" variant="light" width="medium" /></Link>
                <Link to="/signup"><Button name="Commencer" variant="dark" width="medium" /></Link>
            </nav>
        </header>
    );
}

export default Header;