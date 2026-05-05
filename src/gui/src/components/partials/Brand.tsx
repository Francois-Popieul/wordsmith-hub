import { Link } from "react-router";
import "./Brand.css";

interface BrandProps {
    variant: "light" | "dark";
    width?: "small" | "large";
    onClick?: () => void;
}

function Brand({ variant, width, onClick }: BrandProps) {
    return (
        <Link className="no_decoration" to="/">
            <div className="brand_container" onClick={onClick}>
                <p><img className={`brand_logo_${variant} brand_logo_${width}`} src="./logo.png" alt="Logo de Wordsmith Hub" /></p>
                <span className={`brand_name_${variant} brand_name_${width}`}>Wordsmith Hub</span>
            </div>
        </Link>
    );
}

export default Brand;