import "./HomepageFeature.css";
import { LuCircleCheckBig } from "react-icons/lu";

interface HomepageFeatureProps {
    title: string;
    text: string;
}

function HomepageFeature({ title, text }: HomepageFeatureProps) {
    return (
        <div className="feature_container">
            <LuCircleCheckBig className="feature_icon" size={24} />
            <div className="feature_presentation_container">
                <h4 className="feature_title">{title}</h4>
                <p className="feature_text">{text}</p>
            </div>
        </div>
    );
}

export default HomepageFeature;