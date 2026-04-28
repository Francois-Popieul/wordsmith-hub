import "./Brand.css";

interface BrandProps {
    variant: "light" | "dark";
    width?: "small" | "large";
    onClick?: () => void;
}

function Brand({ variant, width, onClick }: BrandProps) {
    return (
        <div className="brand_container" onClick={onClick}>
            <p><img className={`brand_logo_${variant} brand_logo_${width}`} src="./logo.png" alt="Logo de Wordsmith Hub" /></p>
            <p className={`brand_name_${variant} brand_name_${width}`}>Wordsmith Hub</p>
        </div>
    );
}

export default Brand;