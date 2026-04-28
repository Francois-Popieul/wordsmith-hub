import "./HomepageVignette.css";
import { type ReactNode } from "react";

interface HomepageVignetteProps {
    title: string;
    text: string;
    icon: ReactNode;
}

function HomepageVignette({ title, text, icon }: HomepageVignetteProps) {
    return (
        <div className="vignette_container">
            {icon}
            <h3 className="vignette_title">{title}</h3>
            <p className="vignette_text">{text}</p>
        </div>
    );
}

export default HomepageVignette;