import "./HomepageFeature.css";
import { CheckIcon } from "../../assets/icons/icons";

interface HomepageFeatureProps {
    title: string;
    text: string;
}

function HomepageFeature({ title, text }: HomepageFeatureProps) {
    return (
        <div className="feature_container">
            <div className="feature_icon_container"><CheckIcon className="feature_icon" size={24} /></div>
            <div className="feature_presentation_container">
                <h4 className="feature_title">{title}</h4>
                <p className="feature_text">{text}</p>
            </div>
        </div>
    );
}

export default HomepageFeature;