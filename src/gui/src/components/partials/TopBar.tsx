import "./TopBar.css";
import AppBurgerMenu from "../ui/AppBurgerMenu";
import Brand from "./Brand";

function TopBar() {

    return (
        <div className="topbar">
            <Brand variant="light" width="small" />
            <AppBurgerMenu />
        </div>
    );
}

export default TopBar;